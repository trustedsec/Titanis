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
	public sealed class ServicePrincipalName : IEquatable<ServicePrincipalName?>
	{
		/// <summary>
		/// Initializes a new <see cref="ServicePrincipalName"/>.
		/// </summary>
		/// <param name="serviceClass">Name of service</param>
		/// <param name="serviceInstance">Service instance name (usually the host name)</param>
		public ServicePrincipalName(string serviceClass, string serviceInstance)
		{
			ServiceClass = serviceClass;
			ServiceInstance = serviceInstance;
		}

		/// <summary>
		/// Gets the name of the service.
		/// </summary>
		public string ServiceClass { get; }
		/// <summary>
		/// Gets the service instance name (usually the host).
		/// </summary>
		public string ServiceInstance { get; }

		private static readonly Regex rgxSpn = new Regex(@"^(?<s>[^/]+)/(?<h>.*)$");
		public static ServicePrincipalName Parse(string text)
		{
			if (string.IsNullOrEmpty(text)) throw new ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));

			var m = rgxSpn.Match(text);
			if (!m.Success)
				throw new FormatException("The SPN must be specified as <service>/<host>");

			var s = m.Groups["s"].Value;
			var h = m.Groups["h"].Value;
			return new ServicePrincipalName(s, h);
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

		public static bool operator ==(ServicePrincipalName? left, ServicePrincipalName? right)
		{
			return EqualityComparer<ServicePrincipalName>.Default.Equals(left, right);
		}

		public static bool operator !=(ServicePrincipalName? left, ServicePrincipalName? right)
		{
			return !(left == right);
		}
	}

	public static class ServiceClassNames
	{
		public const string Cifs = "cifs";
		public const string Host = "host";
		public const string RestrictedKrbHost = "RestrictedKrbHost";
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
