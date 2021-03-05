using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.Msrpc.Mswmi.Wmio;

namespace Titanis.Msrpc.Mswmi
{
	public class WmiMethodBuilder
	{
		public const string ReturnValueName = "ReturnValue";
		public const string ParametersClassName = "__PARAMETERS";

		public WmiMethodBuilder(string name, CimType returnType)
		{
			this.Name = name;
			this.ReturnType = returnType;

			this.InputParams = new WmiClassBuilder(ParametersClassName, null);
			this.InputParams.Qualifiers.Add(QualifierFactory.Abstract());
			this.OutputParams = new WmiClassBuilder(ParametersClassName, null);
			this.OutputParams.Qualifiers.Add(QualifierFactory.Abstract());
		}

		internal int classIndex;
		public string Name { get; }
		public CimType ReturnType { get; }
		public WmiClassBuilder InputParams { get; }
		public WmiClassBuilder OutputParams { get; private set; }
		public List<WmiQualifier> Qualifiers { get; } = new List<WmiQualifier>();

		internal MethodDescription Build(ByteWriter methodHeapWriter)
		{
			if (this.ReturnType != CimType.None)
			{
				this.OutputParams.DefineProperty(ReturnValueName, this.ReturnType, null,
					QualifierFactory.CIMTYPE(this.ReturnType.ToString().ToLower()),
					QualifierFactory.Out()
					);
				//new WmiQualifier("ID", WmiQualifierFlavor.None, CimType.SInt32, this.InputParams.Properties.Count + this.OutputParams.Properties.Count)
			}

			return new MethodDescription()
			{
				nameRef = methodHeapWriter.WriteHeapString(this.Name),
				// TODO: Set for inherited methods
				flags = WmiQualifierFlavor.None,
				methodOriginIndex = (uint)this.classIndex,
				qualHeapRef = methodHeapWriter.WriteHeapStruct(new QualifierSet(this.Qualifiers.ConvertAll(r => r.Encode(methodHeapWriter)).ToArray())),
				inputSig = methodHeapWriter.WriteHeapStruct(new MethodSignatureBlock
				{
					obj = new ObjectBlock(this.InputParams.BuildClass())
				}),
				outputSig = methodHeapWriter.WriteHeapStruct(new MethodSignatureBlock
				{
					obj = new ObjectBlock(this.OutputParams.BuildClass())
				})
			};
		}
	}
}
