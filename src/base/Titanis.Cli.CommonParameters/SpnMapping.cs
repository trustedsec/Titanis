using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Titanis.Security;

namespace Titanis.Cli
{
	[Flags]
	public enum SpnMappingOptions
	{
		None = 0,
		Revert = 1,
	}

    [TypeConverter(typeof(SpnMappingConverter))]
	public class SpnMapping
	{
		public SpnMapping(SecurityPrincipalName matchName, SecurityPrincipalName replaceName, SpnMappingOptions options)
		{
			ArgumentNullException.ThrowIfNull(matchName);
			ArgumentNullException.ThrowIfNull(replaceName);
			this.MatchName = matchName;
			this.ReplaceName = replaceName;
			this.Options = options;
		}

		public SecurityPrincipalName MatchName { get; }
		public SecurityPrincipalName ReplaceName { get; }
		public SpnMappingOptions Options { get; }

		public override string ToString() => $"{this.MatchName} => {this.ReplaceName}";

		private static bool Matches(string str, string? pattern) => string.IsNullOrEmpty(pattern) || pattern == "*" || pattern.Equals(str, StringComparison.OrdinalIgnoreCase);
		private static string? Replace(string? str, string? replace) => (string.IsNullOrEmpty(replace) || replace == "*") ? str : replace;

		public bool Matches(SecurityPrincipalName spn)
		{
			ArgumentNullException.ThrowIfNull(spn);

			bool matches =
				(
					(spn is ServicePrincipalName svcpn)
					&& (this.MatchName is ServicePrincipalName matchSvc)
					&& Matches(svcpn.ServiceClass, matchSvc.ServiceClass)
				) || (
					(spn is UserPrincipalName upn)
					&& (this.MatchName is UserPrincipalName matchUpn)
					&& Matches(upn.UserName, matchUpn.UserName)
					&& Matches(upn.Realm, matchUpn.Realm)
					);
			return matches;
		}
		public SecurityPrincipalName Map(SecurityPrincipalName spn)
		{
			ArgumentNullException.ThrowIfNull(spn);
			if ((spn is ServicePrincipalName svcpn))
			{
				if (this.ReplaceName is ServicePrincipalName replaceSvc)
				{
					string sc = Replace(svcpn.ServiceClass, replaceSvc.ServiceClass);
					string si = Replace(svcpn.ServiceInstance, replaceSvc.ServiceInstance);
					return new ServicePrincipalName(svcpn.NameType, sc, si);
				}
				else
					return this.ReplaceName;
			}
			else if ((spn is UserPrincipalName upn))
			{
				if (this.ReplaceName is UserPrincipalName replaceUpn)
				{
					var realm = Replace(upn.Realm, replaceUpn.Realm);
					var uname = Replace(upn.UserName, replaceUpn.UserName);
					return new UserPrincipalName(uname, realm);
				}
				else
					return this.ReplaceName;
			}
			else
				return spn;
		}
	}

	partial class SpnMappingConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
			=> (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);
		public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
			{
				var match = rgxMapping.Match(str);
				if (
					match.Success
					&& SecurityPrincipalName.TryParse(match.Groups["match"].Value, out var matchName)
					&& SecurityPrincipalName.TryParse(match.Groups["replace"].Value, out var replaceName))
				{
					SpnMappingOptions options = SpnMappingOptions.None;
					if (match.Groups["r"].Success)
						options |= SpnMappingOptions.Revert;

					return new SpnMapping(matchName, replaceName, options);
				}
				else
				{
					throw new ArgumentException($"The SPN mapping must be of the format <serviceClass>/<serviceInstance>[~]=<serviceClass>/<serviceInstance>");
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		private static readonly Regex rgxMapping = SpnMappingRegex();

		[GeneratedRegex(@"^(?<match>(~[^=]|[^~=])+)(?<r>~)?=(?<replace>.*)?$")]
		private static partial Regex SpnMappingRegex();
	}

}
