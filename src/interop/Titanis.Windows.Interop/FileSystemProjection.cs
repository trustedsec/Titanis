using System.ComponentModel.Design;
using Windows.Win32.Foundation;
using Windows.Win32.Storage.ProjectedFileSystem;

namespace Titanis.Windows.Interop.ProjectedFileSystem
{
	public class FileSystemProjection
	{
		private readonly string _rootDirectory;

		private readonly PRJ_CALLBACKS _callbacks;

		public FileSystemProjection(string rootDirectory)
		{
			this._rootDirectory = rootDirectory;

			this._callbacks = new PRJ_CALLBACKS()
			{
				StartDirectoryEnumerationCallback = this.StartDirectoryEnumerationCallback,
				EndDirectoryEnumerationCallback = this.EndDirectoryEnumerationCallback,
				GetDirectoryEnumerationCallback = this.GetDirectoryEnumerationCallback,
				GetPlaceholderInfoCallback = this.GetPlaceholderInfoCallback,
				GetFileDataCallback = this.GetFileDataCallback,
				QueryFileNameCallback = this.QueryFileNameCallback,
				NotificationCallback = this.NotificationCallback,
				CancelCommandCallback = this.CancelCommandCallback,
			};
		}

		private object _opLock = new object();

		class Operation
		{
			private readonly int commandId;
			private DateTime startTime;

			internal Operation(int commandId)
			{
				this.commandId = commandId;
				this.startTime = DateTime.UtcNow;
			}
		}
		private Dictionary<int, Operation> _pendingOperations = new Dictionary<int, Operation>();

		private static int GetResultCodeForException(Exception ex)
		{
			throw new NotImplementedException();
		}
		private int BeginOperation(int commandId, Func<Task> func)
		{
			Task task;
			try
			{
				task = func();
			}
			catch (Exception ex)
			{
				return GetResultCodeForException(ex);
			}

			if (task.IsCompleted)
			{
				if (task.Exception != null)
				{
					return GetResultCodeForException(task.Exception);
				}
				else
				{
					return 0;
				}
			}
			else
			{
				var op = new Operation(commandId);
				lock (this._opLock)
				{
					this._pendingOperations[commandId] = op;
				}

				return ERROR_IO_PENDING;
			}
		}

		private int StartDirectoryEnumerationCallback(ref readonly PRJ_CALLBACK_DATA callbackData, ref readonly Guid enumerationId)
		{
			return this.BeginOperation(callbackData.CommandId, () => this.StartDirectoryEnum(callbackData.FilePathName, enumerationId));
		}

		private int EndDirectoryEnumerationCallback(ref readonly PRJ_CALLBACK_DATA callbackData, ref readonly Guid enumerationId)
		{
			throw new NotImplementedException();
		}

		private int GetDirectoryEnumerationCallback(ref readonly PRJ_CALLBACK_DATA callbackData, ref readonly Guid enumerationId, string searchExpression, PRJ_DIR_ENTRY_BUFFER_HANDLE dirEntryBufferHandle)
		{
			throw new NotImplementedException();
		}

		private int GetPlaceholderInfoCallback(ref readonly PRJ_CALLBACK_DATA callbackData)
		{
			throw new NotImplementedException();
		}

		private int GetFileDataCallback(ref readonly PRJ_CALLBACK_DATA callbackData, ulong byteOffset, uint length)
		{
			throw new NotImplementedException();
		}

		private int QueryFileNameCallback(ref readonly PRJ_CALLBACK_DATA callbackData)
		{
			throw new NotImplementedException();
		}

		private int NotificationCallback(ref readonly PRJ_CALLBACK_DATA callbackData, bool isDirectory, PRJ_NOTIFICATION notification, string destinationFileName, ref PRJ_NOTIFICATION_PARAMETERS operationParameters)
		{
			throw new NotImplementedException();
		}

		private void CancelCommandCallback(ref readonly PRJ_CALLBACK_DATA callbackData)
		{
			throw new NotImplementedException();
		}

		public void Start()
		{
			int res = NativeMethods.PrjStartVirtualizing(
				this._rootDirectory,
				this._callbacks,
				IntPtr.Zero,
				null,
				out var nsctx
				);
		}
	}
}
