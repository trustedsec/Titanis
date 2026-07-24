using ms_wmi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Titanis.Msrpc.Mswmi;

namespace Titanis.Cli.WmiTool;

/// <task category="WMI;Enumeration;Lateral Movement">Invoke a method on a WMI class or object</task>
[Command]
[Description("Invokes a method on a WMI class or object")]
[DetailedHelpText(@"For each object, {0} looks up the specified method and parses/coerces the command line arguments after the method name as arguments to the WMI method.

To pass an array of values to a WMI method, enter each element as a separate command line argument (separated by spaces) with [ before the first element and ] after the last argument.  For example, to invoke this method:

	void WmiMethod(string argFirst, int[] values, string argLast)

you would enter:

	Wmi invoke ... WmiMethod ""first arg"" [ 1 2 3 4 5 ] ""last arg""
")]
[Example("Start EXPLORER.EXE", "{0} -namespace root\\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-DC1 Win32_Process Create C:\\WINDOWS\\explorer.exe")]
[Example("Terminate a process by PID", "{0} -namespace root\\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-DC1 Win32_Process.Handle=8008 Terminate")]
[Example("Terminate a process by name", "{0} -namespace root\\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-DC1 \"SELECT * FROM Win32_Process WHERE Caption='REGEDIT.EXE'\" Terminate")]
internal class InvokeCommand : WmiObjectCommandBase
{
	[Parameter(20)]
	[Mandatory]
	[Description("Method to invoke")]
	public string Method { get; set; }

	[Parameter(30)]
	[Description("Arguments to pass to the method")]
	public string[] Arguments { get; set; }

	[Parameter]
	[Advanced]
	[Description("List of parameters to skip")]
	public string[] SkipParams { get; set; }

	private string? _lastOrigin;

	protected sealed override async Task ProcessObject(WmiObject obj, WmiScope scope, CancellationToken cancellationToken)
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
			throw new NotSupportedException($"The returned WMI object type ({obj.GetType().FullName}) is not supported.");

		var method = klass.GetMethod(this.Method);
		if (method == null)
			throw new Exception($"The WMI class does not have a method named '{this.Method}'.");

		Dictionary<string, object?> args = new Dictionary<string, object?>();
		var inputProps = method.InputSignature?.Properties ?? Array.Empty<WmiProperty>();
		Array.Sort(inputProps, (x, y) => (x.Id ?? x.DeclarationOrder).CompareTo(y.Id ?? y.DeclarationOrder));

		bool ShouldSkip(WmiProperty prop)
		{
			return (this.SkipParams != null && this.SkipParams.Contains(prop.Name, StringComparer.OrdinalIgnoreCase));
		}

		bool argFailed = false;
		if (this.Arguments != null)
		{
			int argPos = 0;
			for (int i = 0; i < this.Arguments.Length; i++)
			{
				var arg = this.Arguments[i];
				if (argPos < inputProps.Length)
				{
					var inProp = inputProps[argPos];
					while (ShouldSkip(inProp))
						inProp = inputProps[++argPos];

					this.WriteDiagnostic($"Parsing WMI method parameter '{inProp.Name}': {arg}");

					if (inProp.PropertyType.IsArray())
					{
						List<object?> elems = new List<object?>();
						if (arg != "[")
							this.WriteError($"Arg #{argPos} ({inProp.Name}) requires an array.  To specify an array, specify a [ by itself, each array element separated by a space, then a ] to mark the end of the array");
						while (++i < this.Arguments.Length && (arg = this.Arguments[i]) != "]")
						{
							this.WriteDiagnostic($"Parsing WMI method parameter '{inProp.Name}[{elems.Count}]': {arg}");

							if (TryParseArg(arg, inProp, out var coerced))
								elems.Add(coerced);
							else
								argFailed = true;
						}
						if (!argFailed)
						{
							var elemType = inProp.ElementType;
							Array arr = Array.CreateInstance(elemType, elems.Count);
							for (int j = 0; j < elems.Count; j++)
							{
								var elem = elems[j];
								arr.SetValue(elem, j);
							}
							args.Add(inProp.Name, arr);
						}
					}
					else
					{
						if (TryParseArg(arg, inProp, out var coerced))
							args.Add(inProp.Name, coerced);
						else
							argFailed = true;
					}

					argPos++;
				}
			}
		}

		if (argFailed)
		{
			this.WriteError("One or more arguments could not be parsed.");
		}
		else
		{
			var res = await obj.InvokeMethodAsync(method.Name, args, cancellationToken);
			if (res != null)
			{
				this.WriteRecord(res);
			}
		}
	}

	private bool TryParseArg(string arg, WmiProperty inProp, out object? coerced)
	{
		try
		{
			coerced = CoerceValue(arg, inProp.PropertyType & CimType.BaseTypeMask, inProp.SubtypeCode);
			return true;
		}
		catch (Exception ex)
		{
			this.WriteError($"Error parsing argument '{arg}' for parameter '{inProp.Name}': {ex.Message}");
			coerced = null;
			return false;
		}
	}

	public static object? CoerceValue(string text, CimType propType, CimSubtype subtype)
	{
		var baseType = propType & CimType.BaseTypeMask;
		var elemType = WmiProperty.GetRuntimeTypeFor(baseType, subtype);
		bool isArray = 0 != (propType & CimType.Array);
		if (isArray)
		{
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
