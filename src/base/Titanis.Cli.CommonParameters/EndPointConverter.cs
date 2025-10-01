using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Cli
{
	/// <summary>
	/// Specifies the default port for a parameter property of type <see cref="EndPoint"/>.
	/// </summary>
	public sealed class DefaultPortAttribute : Attribute
	{
		public DefaultPortAttribute(int port)
		{
			DefaultPort = port;
		}

		public int DefaultPort { get; }
	}

	public class EndPointConverter : TypeConverter
	{
		public sealed override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			else
				return base.CanConvertFrom(context, sourceType);
		}

		public sealed override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
			{
				if (string.IsNullOrEmpty(str))
					return null;

				string host;
				int port;
				int isep = str.LastIndexOf(':');
				if (isep == -1)
				{
					host = str;
					port = -1;
				}
				else if (int.TryParse(str.AsSpan(isep + 1), out port))
				{
					host = str.Substring(0, isep);
				}
				else
				{
					throw new FormatException($"The endpoint must either be of the form <host or IP> or <host or IP>:<port#>: {str}");
				}

				if (port == -1 && (context is ParameterConverterContext paramContext))
				{
					var defaultPortAtr = paramContext.Parameter.Property.GetCustomAttribute<DefaultPortAttribute>(true);
					if (defaultPortAtr is null)
						throw new FormatException($"The endpoint '{str}' for parameter '{paramContext.Parameter.Name}' does not specify a port, and the parameter does not have a default port set.");
					port = defaultPortAtr.DefaultPort;
				}

				if (IPAddress.TryParse(host, out IPAddress ipaddr))
					return new IPEndPoint(ipaddr, port);
				else
					return new DnsEndPoint(host, port);
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
