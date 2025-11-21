using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Ldap;

namespace Ldap;

record class SyntaxItem(string syntaxKey, string memberName)
{

}
[Command]
[Description("Lists AD syntaxes")]
[OutputRecordType(typeof(SyntaxItem), DefaultOutputStyle = OutputStyle.Csv)]
[DetailedHelpText(@"A syntax describes the format of data within an attribute value and specifies how the raw bytes are decoded into the logical value.")]
internal class ListSyntaxCommand : Command
{
	protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
	{
		var klass = typeof(AdSyntaxes);
		var fields = klass.GetFields();
		foreach (var field in fields)
		{
			if (field.FieldType.IsAssignableTo(typeof(LdapSyntax)))
			{
				var syntax = (LdapSyntax)field.GetValue(null);
				this.WriteRecord(new SyntaxItem($"{syntax.ActiveDirectoryOid}-{syntax.OmSyntax}-{syntax.OmObjectClass}", field.Name));
			}
		}

		return 0;
	}
}
