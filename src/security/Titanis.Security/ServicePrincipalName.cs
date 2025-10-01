using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Titanis.Security
{
	/// <summary>
	/// Represents a service principal name.
	/// </summary>
	[TypeConverter(typeof(ServicePrincipalNameConverter))]
	public sealed class ServicePrincipalName : SecurityPrincipalName, IEquatable<ServicePrincipalName?>
	{
		/// <summary>
		/// Initializes a new <see cref="ServicePrincipalName"/>.
		/// </summary>
		/// <param name="serviceClass">Name of service</param>
		/// <param name="serviceInstance">Service instance name (usually the host name)</param>
		public ServicePrincipalName(string serviceClass, string serviceInstance)
		{
			ServiceClass = serviceClass;
			_instance = serviceInstance;
		}
		/// <summary>
		/// Initializes a new <see cref="ServicePrincipalName"/>.
		/// </summary>
		/// <param name="serviceClass">Name of service</param>
		/// <param name="serviceInstanceParts">Parts of the service instance name (usually the host name)</param>
		public ServicePrincipalName(string serviceClass, string[] serviceInstanceParts)
		{
			if (serviceInstanceParts is null || serviceInstanceParts.Length == 0)
				throw new ArgumentNullException(nameof(serviceInstanceParts));

			ServiceClass = serviceClass;
			_instanceParts = serviceInstanceParts;
		}
		internal ServicePrincipalName(string serviceClass, string serviceInstance, string[] parts)
		{
			ServiceClass = serviceClass;
			_instance = serviceInstance;
			_instanceParts = parts;
		}

		/// <summary>
		/// Gets the name of the service.
		/// </summary>
		public string ServiceClass { get; }

		private string? _instance;
		private string[]? _instanceParts;

		/// <summary>
		/// Gets the service instance name (usually the host).
		/// </summary>
		/// <remarks>
		/// If the service instance name has multiple parts, this string returns the parts joined with an intervening <c>/</c>.
		/// </remarks>
		public string ServiceInstance => (this._instance ??= string.Join("/", this._instanceParts));
		/// <summary>
		/// Gets the parts of the service instance name (usually the host).
		/// </summary>
		public string[] ServiceInstanceParts => (this._instanceParts ??= this._instance!.Split('/'));

		/// <inheritdoc/>
		public sealed override PrincipalNameType NameType => PrincipalNameType.ServiceInstance;
		/// <inheritdoc/>
		public sealed override string[] GetNameParts() => new string[] { this.ServiceClass, this.ServiceInstance };
		/// <inheritdoc/>
		public sealed override int NamePartCount => 2;
		/// <inheritdoc/>
		public sealed override string GetNamePart(int index) => index switch
		{
			0 => this.ServiceClass,
			1 => this.ServiceInstance,
			_ => throw new ArgumentOutOfRangeException(nameof(index))
		};

		public static bool TryParse(string? text, out ServicePrincipalName? spn)
		{
			if (!string.IsNullOrEmpty(text))
			{
				int isep = text.IndexOf('/');
				if (isep > 0)
				{
					int isep0 = isep;

					List<string> instanceParts = new List<string>();
					do
					{
						isep++;
						int isep2 = text.IndexOf('/', isep);
						string part = isep2 == -1 ? text.Substring(isep) : text.Substring(isep, isep2 - isep);
						isep = isep2;
						instanceParts.Add(part);
					} while (isep > 0);

					var serviceClass = text.Substring(0, isep0);
					var instance = text.Substring(isep0 + 1);

					spn = new ServicePrincipalName(serviceClass, instance, instanceParts.ToArray());
					return true;
				}
			}

			spn = null;
			return false;
		}
		public static ServicePrincipalName Parse(string text)
		{
			if (string.IsNullOrEmpty(text)) throw new ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));

			if (TryParse(text, out var spn))
				return spn;

			throw new FormatException($"The value '{text}' is not a valid SPN.  The SPN must be specified as <service>/<host>");
		}

		private string? _str;

		/// <inheritdoc/>
		public sealed override string ToString()
			=> (this._str ??= $"{ServiceClass}/{ServiceInstance}");

		public sealed override bool Equals(object? obj)
		{
			return Equals(obj as ServicePrincipalName);
		}

		public bool Equals(ServicePrincipalName? other)
		{
			return other is not null &&
				   ServiceClass.Equals(other.ServiceClass, StringComparison.OrdinalIgnoreCase) &&
				   ServiceInstance.Equals(other.ServiceInstance, StringComparison.OrdinalIgnoreCase);
		}

		public sealed override int GetHashCode()
		{
			return System.HashCode.Combine(ServiceClass.GetHashCode(StringComparison.OrdinalIgnoreCase), ServiceInstance.GetHashCode(StringComparison.OrdinalIgnoreCase));
		}

		public ServicePrincipalName WithServiceClass(string serviceClass)
		{
			if (string.IsNullOrEmpty(serviceClass)) throw new ArgumentException($"'{nameof(serviceClass)}' cannot be null or empty.", nameof(serviceClass));
			return new ServicePrincipalName(serviceClass, this.ServiceInstance);
		}

		public ServicePrincipalName WithServiceInstance(string serviceInstance)
		{
			if (string.IsNullOrEmpty(serviceInstance)) throw new ArgumentException($"'{nameof(serviceInstance)}' cannot be null or empty.", nameof(serviceInstance));
			return new ServicePrincipalName(this.ServiceClass, serviceInstance);
		}

		public static bool operator ==(ServicePrincipalName? left, ServicePrincipalName? right)
		{
			return EqualityComparer<ServicePrincipalName>.Default.Equals(left, right);
		}

		public static bool operator !=(ServicePrincipalName? left, ServicePrincipalName? right)
		{
			return !(left == right);
		}
	}

	/// <summary>
	/// Names of common service classes.
	/// </summary>
	/// <remarks>
	/// Note that the inconsistency in casing is intentional and mirrors what is observed in real network interactions.
	/// Some names, such <c>host</c>, are cased differently by different components.
	/// </remarks>
	public static class ServiceClassNames
	{
		public const string Cifs = "cifs";
		public const string HostU = "HOST";
		public const string HostL = "host";
		public const string RestrictedKrbHost = "RestrictedKrbHost";
		public const string Rpc = "RPC";
		public const string RpcSs = "RPCSS";
		public const string Krbtgt = "krbtgt";
	}

	public sealed class ServicePrincipalNameConverter : TypeConverter
	{
		public sealed override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		public sealed override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
				return ServicePrincipalName.Parse(str);
			else
				return base.ConvertFrom(context, culture, value);
		}
	}
}
