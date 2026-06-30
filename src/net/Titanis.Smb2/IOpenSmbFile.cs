using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Smb2.Pdus;
using Titanis.IO;
using System.Runtime.CompilerServices;

namespace Titanis.Smb2
{
	public interface IOpenSmbFile
	{
		Task<Smb2OpenFileObjectBase> CreateFileAsync(
			string fileName,
			Smb2CreateInfo createInfo,
			FileAccess access,
			CancellationToken cancellationToken);
	}

	public static class SmbOpenFileExtensions
	{
		public static async Task<Smb2FileStream> CreateFileAsync(this IOpenSmbFile source, string path, FileMode mode, CancellationToken cancellationToken)
		{
			var access = (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite;
			return new Smb2FileStream((Smb2OpenFile)await source.CreateFileAsync(path, Smb2CreateInfo.ForCreateOrOpenFile(mode, access, FileShare.Read), access, cancellationToken).ConfigureAwait(false), access, true);
		}

		public static async Task<Smb2FileStream> CreateFileAsync(this IOpenSmbFile source, string path, FileMode mode, FileAccess access, CancellationToken cancellationToken)
		{
			return new Smb2FileStream((Smb2OpenFile)await source.CreateFileAsync(path, Smb2CreateInfo.ForCreateOrOpenFile(mode, access, FileShare.Read), access, cancellationToken).ConfigureAwait(false), access, true);
		}

		public static async Task<Smb2FileStream> CreateFileAsync(this IOpenSmbFile source, string path, FileMode mode, FileAccess access, FileShare share, CancellationToken cancellationToken, Smb2FileCreateOptions extraOptions = Smb2FileCreateOptions.None)
		{
			return new Smb2FileStream((Smb2OpenFile)await source.CreateFileAsync(path, Smb2CreateInfo.ForCreateOrOpenFile(mode, access, share, extraOptions), access, cancellationToken).ConfigureAwait(false), access, true);
		}



		public static async Task<Smb2FileStream> CreateFileAsync(this IOpenSmbFile source, string path, CancellationToken cancellationToken, Smb2FileCreateOptions extraOptions = Smb2FileCreateOptions.None)
		{
			return new Smb2FileStream((Smb2OpenFile)await source.CreateFileAsync(path, Smb2CreateInfo.ForCreateOrOpenFile(FileMode.Create, FileAccess.ReadWrite, FileShare.None, extraOptions: extraOptions), FileAccess.ReadWrite, cancellationToken).ConfigureAwait(false), FileAccess.ReadWrite, true);
		}

		public static async Task<Smb2FileStream> OpenFileReadAsync(this IOpenSmbFile source, string path, CancellationToken cancellationToken)
		{
			return new Smb2FileStream((Smb2OpenFile)await source.CreateFileAsync(path, Smb2CreateInfo.ForCreateOrOpenFile(FileMode.Open, FileAccess.Read, FileShare.Read), FileAccess.Read, cancellationToken).ConfigureAwait(false), FileAccess.Read, true);
		}

		public static async Task<Smb2FileStream> OpenFileWriteAsync(this IOpenSmbFile source, string path, CancellationToken cancellationToken)
		{
			return new Smb2FileStream((Smb2OpenFile)await source.CreateFileAsync(path, Smb2CreateInfo.ForCreateOrOpenFile(FileMode.OpenOrCreate, FileAccess.Write, FileShare.None), FileAccess.Write, cancellationToken).ConfigureAwait(false), FileAccess.Write, true);
		}




		public static async Task<StreamReader> OpenTextFile(this IOpenSmbFile source, string path, CancellationToken cancellationToken)
		{
			return new StreamReader(await source.CreateFileAsync(path, FileMode.Open, FileAccess.Read, FileShare.Read, cancellationToken).ConfigureAwait(false));
		}

		public static async Task<StreamWriter> CreateTextFile(this IOpenSmbFile source, string path, CancellationToken cancellationToken)
		{
			return new StreamWriter(await source.CreateFileAsync(path, FileMode.Create, FileAccess.Write, FileShare.Read, cancellationToken).ConfigureAwait(false));
		}

		public static async Task<StreamWriter> AppendTextFile(this IOpenSmbFile source, string path, CancellationToken cancellationToken)
		{
			return new StreamWriter(await source.CreateFileAsync(path, FileMode.Append, FileAccess.Write, FileShare.Read, cancellationToken).ConfigureAwait(false));
		}

		public static async Task DeleteDirectoryAsync(this IOpenSmbFile source, string path, CancellationToken cancellationToken)
		{
			var file = await source.CreateFileAsync(path, Smb2CreateInfo.ForRemoveDirectory(), FileAccess.ReadWrite, cancellationToken).ConfigureAwait(false);
			await file.CloseAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Copies a local file to a UNC path over SMB2.
		/// </summary>
		/// <param name="sourceFileName">Local path of the source file</param>
		/// <param name="destinationFileName">Destination UNC path</param>
		/// <param name="overwrite"><see langword="true"/> to overwrite the file at <paramref name="destinationFileName"/> if it exists</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>
		/// A <see cref="Smb2CreateAction"/> value indicating whether the file existed.
		/// </returns>
		/// <remarks>
		/// In addition to copying the file data, this method also copies the file write time
		/// and file attributes.
		/// </remarks>
		public static async Task<Smb2CreateAction> PutFileAsync(
			this IOpenSmbFile source,
			string sourceFileName,
			string destinationFileName,
			bool overwrite,
			CancellationToken cancellationToken,
			int chunkSize = Smb2Client.DefaultChunkSize)
		{
			if (string.IsNullOrEmpty(sourceFileName)) throw new ArgumentException($"'{nameof(sourceFileName)}' cannot be null or empty.", nameof(sourceFileName));
			ArgumentNullException.ThrowIfNull(destinationFileName);

			// Open the source file
			using (var sourceStream = File.OpenRead(sourceFileName))
			{
				// Get source file attributes
				Winterop.FileAttributes attrs = string.IsNullOrEmpty(sourceFileName)
					? Winterop.FileAttributes.Normal
					: (Winterop.FileAttributes)File.GetAttributes(sourceFileName);

				// Check whether the remote target is a directory
				bool isDestDir = false;
				bool fileExists = false;
				try
				{
					var file = await source.CreateFileAsync(destinationFileName, new Smb2CreateInfo
					{
						OplockLevel = Smb2OplockLevel.None,
						ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
						DesiredAccess = (uint)Smb2FileAccessRights.ReadAttributes,
						FileAttributes = 0,
						ShareAccess = Smb2ShareAccess.ReadWriteDelete,
						CreateDisposition = Smb2CreateDisposition.OpenExisting,
						CreateOptions = Smb2FileCreateOptions.OpenReparsePoint,
						RequestMaximalAccess = true,
						QueryOnDiskId = true
					}, FileAccess.Read, cancellationToken).ConfigureAwait(false);

					await using (file)
					{
						isDestDir = file.IsDirectory;
						if (isDestDir)
						{
							string sourceFilePart = Path.GetFileName(sourceFileName);
							destinationFileName = UncPath.Combine(destinationFileName, Path.GetFileName(sourceFilePart));
						}
						else
						{
							fileExists = true;
						}
					}
				}
				catch { }

				// Check whether remote target exists
				// If the user-provided name is a directory, the previous step appended the path name
				if (!fileExists && isDestDir)
				{
					try
					{
						var file = await source.CreateFileAsync(destinationFileName, new Smb2CreateInfo
						{
							OplockLevel = Smb2OplockLevel.None,
							ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
							DesiredAccess = (uint)Smb2FileAccessRights.ReadAttributes,
							FileAttributes = 0,
							ShareAccess = Smb2ShareAccess.ReadWriteDelete,
							CreateDisposition = Smb2CreateDisposition.OpenExisting,
							CreateOptions = Smb2FileCreateOptions.OpenReparsePoint,
							RequestMaximalAccess = true,
							QueryOnDiskId = true
						}, FileAccess.Read, cancellationToken).ConfigureAwait(false);
						await using (file)
						{
							if (file.IsDirectory)
								throw new IOException($"The target path `{destinationFileName}' is a directory, not a file.");

							fileExists = true;
						}
					}
					catch
					{
						// More of a courtesy, don't report error on this step
					}
				}

				if (fileExists && !overwrite)
					throw new IOException($"The file `{destinationFileName}' already exists.");

				{
					var destStream = await source.CreateFileAsync(destinationFileName, cancellationToken).ConfigureAwait(false);
					await using (destStream)
					{
						var file = destStream.File;
						file.SetAttributes(attrs);
						var createAction = file.CreateAction;
						if (sourceStream.CanSeek)
						{
							await file.SetLengthAsync(sourceStream.Length, cancellationToken).ConfigureAwait(false);
						}

						await sourceStream.CopyToAsync2(destStream, chunkSize, cancellationToken).ConfigureAwait(false);

						DateTime dateTime = File.GetLastWriteTimeUtc(sourceFileName);
						await file.SetBasicInfoAsync(
							null,
							null,
							dateTime,
							dateTime,
							(Winterop.FileAttributes)File.GetAttributes(sourceFileName),
							cancellationToken
							).ConfigureAwait(false);

						if (isDestDir)
							createAction |= Smb2CreateAction.IsDirectory;

						return createAction;
					}
				}
			}
		}
	}
}
