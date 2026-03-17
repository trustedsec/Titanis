using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Titanis.Certificates
{
	public static partial class CertificateHelper
	{
		public static X509Certificate2Collection LoadFrom(byte[] bytes)
		{
			if (bytes.IsNullOrEmpty())
				throw new ArgumentNullException(nameof(bytes));

			LoadContext ctx = new LoadContext();
			LoadFrom(bytes, null, ctx, false);

			return ctx.certs;
		}
		public static X509Certificate2Collection LoadFrom(byte[] bytes, byte[]? keyFileBytes, string? password, bool failOnKeyFailure)
		{
			if (bytes is null || bytes.Length == 0)
				throw new ArgumentNullException(nameof(bytes));

			LoadContext ctx = new LoadContext();
			if (!keyFileBytes.IsNullOrEmpty())
				LoadFrom(keyFileBytes, password, ctx, failOnKeyFailure);
			LoadFrom(bytes, password, ctx, failOnKeyFailure);

			return ctx.certs;
		}

		class LoadContext
		{
			internal X509Certificate2Collection certs = new X509Certificate2Collection();
			internal Dictionary<string, RSA> keysByLocalId = new Dictionary<string, RSA>();
			internal List<RSA> allKeys = new List<RSA>();
		}

		private static void LoadFrom(byte[] bytes, string? password, LoadContext context, bool failOnKeyFailure)
		{
			if (bytes[0] == 0x30)
			{
				// Probably a .pfx file
				context.certs.Import(bytes, password);
				return;
			}
			else
			{
				// Probably a .PEM file of some sort
				TextReader reader = new StreamReader(new MemoryStream(bytes));
				Dictionary<string, string>? bagAttrs = null;
				Dictionary<string, string>? keyAttrs = null;
				Dictionary<string, string>? attrs = null;
				string? localKeyId = null;


				while (reader.Peek() != -1)
				{
					string line = reader.ReadLine()!;

					Match m;
					if ((m = rgxPemHeader.Match(line)).Success && m.Groups["b"].Success)
					{
						// This is an object of some sort

						var objType = m.Groups["t"].Value;
						var end = "-----END " + objType + "-----";

						StringBuilder sb = new StringBuilder();
						while ((reader.Peek() != -1) && ((line = reader.ReadLine()) != end))
						{
							sb.Append(line);
						}

						byte[] objBytes = Convert.FromBase64String(sb.ToString());

						if (objType == "CERTIFICATE")
						{
							X509Certificate2 cert = new X509Certificate2(objBytes);
							if (!cert.HasPrivateKey)
							{
								if (localKeyId != null && context.keysByLocalId.TryGetValue(localKeyId, out var localKey))
								{
									cert = cert.CopyWithPrivateKey(localKey);
								}
								else
								{
									// Try all keys
									foreach (var key in context.allKeys)
									{
										try
										{
											var withPrivateKey = cert.CopyWithPrivateKey(key);
											if (OperatingSystem.IsWindows())
											{
												// NOTE: Windows does not support ephemeral keys loaded from a PEM file, so the workaround is to export to a PFX (in memory) and reimport
												byte[] rawData = withPrivateKey.Export(X509ContentType.Pfx);
												//withPrivateKey = new X509Certificate2(rawData);
											}
											cert = withPrivateKey;
											break;
										}
										catch (ArgumentException ex)
										{
											// The key probably doesn't match
										}
									}
								}
							}
							context.certs.Add(cert);
						}
						else if (objType == "ENCRYPTED PRIVATE KEY")
						{
							if (password != null)
							{
								RSA rsa = RSA.Create();
								rsa.ImportEncryptedPkcs8PrivateKey(Encoding.UTF8.GetBytes(password), objBytes, out int cbRead);

								context.allKeys.Add(rsa);
								if (!string.IsNullOrEmpty(localKeyId))
									context.keysByLocalId.Add(localKeyId, rsa);
							}
							else if (failOnKeyFailure)
							{
								throw new CryptographicException("Failed to decrypt the key with the provided password.");
							}
						}
						else if (objType == "PRIVATE KEY")
						{
							RSA rsa = RSA.Create();
							rsa.ImportPkcs8PrivateKey(objBytes, out int cbRead);

							context.allKeys.Add(rsa);
							if (!string.IsNullOrEmpty(localKeyId))
								context.keysByLocalId.Add(localKeyId, rsa);
						}
						else
						{
							throw new NotImplementedException();
						}

						bagAttrs = null;
						keyAttrs = null;
						attrs = null;
						localKeyId = null;
					}
					else if (line == "Bag Attributes")
					{
						bagAttrs = new Dictionary<string, string>();
						attrs = bagAttrs;
					}
					else if (line == "Key Attributes")
					{
						keyAttrs = new Dictionary<string, string>();
						attrs = keyAttrs;
					}
					else if ((m = rgxPemAttr.Match(line)).Success)
					{
						if (attrs != null)
						{
							var name = m.Groups["n"].Value;
							var value = m.Groups["v"].Value;
							attrs.Add(name, value);

							if (name.Equals("localKeyID", StringComparison.OrdinalIgnoreCase))
								localKeyId = value;

							// TODO: What about duplicates?
						}
						else
						{
							// TODO: Warn?
						}
					}
				}

				return;
			}
			//else
			//{
			//	throw new ArgumentException($"The file '{fileName}' is not recognized as a file containing certificates or keys.");
			//}
		}

		private static Regex rgxPemHeader = PemHeaderPattern();

		[GeneratedRegex(@"^-----((?<b>BEGIN)|(?<e>END))\s+(?<t>[^-]*)-----$")]
		private static partial Regex PemHeaderPattern();


		private static Regex rgxPemAttr = PemAttributePattern();

		[GeneratedRegex(@"^\s+(?<n>[^:]+):\s*(?<v>.*)$")]
		private static partial Regex PemAttributePattern();
	}

	public class PemFileObject
	{
		internal PemFileObject()
		{
		}
	}

	public class PemEncryptedPrivateKey : PemFileObject
	{
		internal PemEncryptedPrivateKey(RSA key)
		{
			Key = key;
		}

		public RSA Key { get; }
	}

	public class PemCertificate : PemFileObject
	{
		internal PemCertificate(X509Certificate2 certificate)
		{
			Certificate = certificate;
		}

		public X509Certificate2 Certificate { get; }
	}

	public class PemOtherObject : PemFileObject
	{
		internal PemOtherObject(byte[] bytes)
		{
			Bytes = bytes;
		}

		public byte[] Bytes { get; }
	}
}
