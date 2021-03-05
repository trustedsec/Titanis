using ms_wmi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titanis;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;

namespace Wmi;

/// <task category="WMI;Enumeration;Lateral Movement">Invoke a method on a WMI class or object</task>
[Command]
[Description("Invokes a method on a WMI class or object")]
[Example("Start EXPLORER.EXE", "{0} -namespace root\\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-DC1 Win32_Process Create C:\\WINDOWS\\explorer.exe")]
[Example("Terminate a process by PID", "{0} -namespace root\\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-DC1 Win32_Process.Handle=8008 Terminate")]
[Example("Terminate a process by name", "{0} -namespace root\\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-DC1 \"SELECT * FROM Win32_Process WHERE Caption='REGEDIT.EXE'\" Terminate")]
internal class InvokeCommand : WmiNamespaceCommandBase
{
	[Parameter(10)]
	[Mandatory]
	[Description("Path of class or object to inspect")]
	public string ObjectPathOrWqlQuery { get; set; }

	[Parameter(20)]
	[Mandatory]
	[Description("Method name")]
	public string Method { get; set; }

	[Parameter(30)]
	[Description("Arguments to pass to the method")]
	public string[] Arguments { get; set; }

	protected sealed override async Task<int> RunAsync(WmiScope ns, CancellationToken cancellationToken)
	{
		if (
			this.ObjectPathOrWqlQuery.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase)
			|| this.ObjectPathOrWqlQuery.StartsWith("ASSOCIATORS OF", StringComparison.OrdinalIgnoreCase)
			)
		{
			var wql = this.ObjectPathOrWqlQuery;
			var query = await ns.ExecuteWqlQueryAsync(wql, 1, cancellationToken);
			while (await query.ReadAsync(cancellationToken))
			{
				try
				{
					await InvokeOn(query.Current, cancellationToken);
				}
				catch (Exception ex)
				{
					this.WriteError($"Method invocation failed: {ex.Message}");
				}
			}
		}
		else
		{
			string objPath = this.ObjectPathOrWqlQuery;
			var obj = await ns.GetObjectAsync(objPath, cancellationToken);
			if (obj != null)
			{
				await InvokeOn(obj, cancellationToken);
			}
			else
			{
				this.WriteError($"Object path `{objPath}' did not return an object.");
			}
		}

		return 0;
	}

	private async Task InvokeOn(WmiObject obj, CancellationToken cancellationToken)
	{
		await InvokeMethodOnObject(obj, cancellationToken);
	}

	private async Task InvokeMethodOnObject(WmiObject obj, CancellationToken cancellationToken)
	{
		WmiClassObject klass;
		WmiObject target;
		if (obj is WmiClassObject klass_)
		{
			klass = klass_;
			target = klass;
		}
		else if (obj is WmiInstanceObject inst)
		{
			target = inst;
			klass = (WmiClassObject)await obj.Scope.GetObjectAsync(inst.WmiClass.Name, cancellationToken);
		}
		else
			throw new NotSupportedException("The returned WMI object type is not supported.");

		var method = klass.GetMethod(this.Method);
		if (method == null)
			throw new Exception($"The WMI class does not have a method named '{this.Method}'.");

		Dictionary<string, object?> args = new Dictionary<string, object?>();
		var inputProps = method.InputSignature?.Properties ?? Array.Empty<WmiProperty>();

		bool argFailed = false;
		if (this.Arguments != null)
		{
			for (int i = 0; i < this.Arguments.Length; i++)
			{
				var arg = this.Arguments[i];
				if (i < inputProps.Length)
				{
					var inProp = inputProps[i];
					try
					{
						var coerced = CoerceValue(arg, inProp.PropertyType, inProp.SubtypeCode);
						args.Add(inProp.Name, coerced);
					}
					catch (Exception ex)
					{
						this.WriteError($"Error parsing argument '{arg}' for parameter '{inProp.Name}': {ex.Message}");
						argFailed = true;
					}
				}
			}
		}

		if (argFailed)
		{
			this.WriteError("One or more arguments could not be parsed.");
		}

		var res = await obj.InvokeMethodAsync(method.Name, args, cancellationToken);
		this.SetOutputFormat(this.ConsoleOutputStyle ?? OutputStyle.List, OutputField.GetFieldsFor(res, this.OutputFields));
		this.WriteRecord(res);
	}

	public static object? CoerceValue(string text, CimType propType, CimSubtype subtype)
	{
		var baseType = propType & CimType.BaseTypeMask;
		var elemType = WmiProperty.GetRuntimeTypeFor(baseType, subtype);
		bool isArray = 0 != (propType & CimType.Array);
		if (isArray)
		{
			//if (value is Array arr)
			//{
			//	var coerced = Array.CreateInstance(elemType, arr.Length);
			//	for (int i = 0; i < coerced.Length; i++)
			//	{
			//		var elem = arr.GetValue(i);
			//		var coercedElem = CoerceElement(elem, elemType);
			//		coerced.SetValue(coercedElem, elem);
			//	}
			//	return coerced;
			//}
			//else
			throw new ArgumentException("An array type requires an array value but an array was not provided.");
		}
		else
		{
			return CoerceElement(text, elemType);
		}
	}

	private static object CoerceElement(string elem, Type elemType)
	{
		if (elemType == typeof(string))
			return elem;

		var converter = Command.GetScalarParamConverter(elemType);
		var converted = converter.ConvertFrom(elem);
		return converted;
	}
}
