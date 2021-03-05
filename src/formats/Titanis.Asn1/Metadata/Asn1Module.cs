using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public class Asn1Module
	{
		public string Name { get; }
		public Asn1Oid? ModuleId { get; }

		public static Asn1Module SystemModule = new Asn1Module(
			"System",
			Asn1Oids.Asn1Modules,
			new Asn1TypeDef[]
			{
				new Asn1TypeDef("BIT-STRING", Asn1Types.BitString),
				new Asn1TypeDef("BOOLEAN", Asn1Types.Boolean),
				new Asn1TypeDef("INTEGER", Asn1Types.Integer),
				new Asn1TypeDef("REAL", Asn1Types.Real),
				new Asn1TypeDef("OCTET-STRING", Asn1Types.OctetString),
				new Asn1TypeDef("NULL", Asn1Types.Null),
				new Asn1TypeDef("SEQUENCE", Asn1Types.Sequence),
				new Asn1TypeDef("OBJECT-IDENTIFIER", Asn1Types.ObjectIdentifier),
				new Asn1TypeDef("RELATIVE-OID", Asn1Types.RelativeOid),
				new Asn1TypeDef("IRI", Asn1Types.Iri),
				new Asn1TypeDef("RELATIVE-IRI", Asn1Types.RelativeIri),
				new Asn1TypeDef("TIME", Asn1Types.Time),
				new Asn1TypeDef("DATE", Asn1Types.Date),
				new Asn1TypeDef("TIME-OF-DAY", Asn1Types.TimeOfDay),
				new Asn1TypeDef("DATE-TIME", Asn1Types.DateTime),
				new Asn1TypeDef("DURATION", Asn1Types.Duration),
				new Asn1TypeDef("BMPString", Asn1Types.BMPString),
				new Asn1TypeDef("GeneralString", Asn1Types.GeneralString),
				new Asn1TypeDef("GraphicString", Asn1Types.GraphicString),
				new Asn1TypeDef("IA5String", Asn1Types.IA5String),
				new Asn1TypeDef("ISO646String", Asn1Types.Iso646String),
				new Asn1TypeDef("NumericString", Asn1Types.NumericString),
				new Asn1TypeDef("PrintableString", Asn1Types.PrintableString),
				new Asn1TypeDef("TeletexString", Asn1Types.TeletexString),
				new Asn1TypeDef("T61String", Asn1Types.T61String),
				new Asn1TypeDef("UniversalString", Asn1Types.UniversalString),
				new Asn1TypeDef("UTF8String", Asn1Types.UTF8String),
				new Asn1TypeDef("VideotexString", Asn1Types.VideotexString),
				new Asn1TypeDef("CharacterString", Asn1Types.CharacterString),
				new Asn1TypeDef("VisibleString", Asn1Types.VisibleString),
				new Asn1TypeDef("GeneralizedTime", Asn1Types.GeneralizedTime),
				new Asn1TypeDef("UTCTime", Asn1Types.UtcTime),
			},
			new Asn1ValueDef[0]
			);

		public void Visit(IModuleVisitor visitor)
		{
			foreach (var type in this.TypeDefinitions)
			{
				type.Visit(visitor);
			}
			foreach (var value in this.ValueDefinitions)
			{
				value.Visit(visitor);
			}
		}

		public Asn1TypeDef[] TypeDefinitions { get; }
		public Asn1ValueDef[] ValueDefinitions { get; }

		private Dictionary<string, Asn1ValueDef> _valuesByName;
		private Dictionary<string, Asn1TypeDef> _typesByName;

		public Asn1Module(
			string name,
			Asn1Oid? moduleId,
			Asn1TypeDef[] types,
			Asn1ValueDef[] values)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException(nameof(name));
			//if (moduleId.IsEmpty)
			//	throw new ArgumentNullException(nameof(moduleId));
			if (!IsValidName(name))
				throw new ArgumentException(string.Format(Messages.Asn1_TypeNameInvalid, name), nameof(name));

			this.Name = name;
			this.ModuleId = moduleId;
			this.TypeDefinitions = types;
			this.ValueDefinitions = values;

			this._valuesByName = values?.ToDictionary(v => v.Name);
			this._typesByName = types?.ToDictionary(t => t.Name);

			foreach (var type in types)
			{
				if (type.Definition.Module == null)
					type.Definition.OnAttaching(this, type.Name);
			}
			foreach (var type in types)
			{
				if (type.Definition.Module == this)
					type.Definition.OnAttached(null, null);
			}
		}

		private List<Asn1Type> _allTypes = new List<Asn1Type>();
		internal void AddType(Asn1Type type)
		{
			Debug.Assert(!this._allTypes.Contains(type));
			this._allTypes.Add(type);
		}

		public Asn1Type[] GetAllTypes() => this._allTypes.ToArray();

		public Asn1TypeDef TryResolveType(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException(nameof(name));

			return this._typesByName?.TryGetValue(name);
		}

		public Asn1ValueDef TryResolveValue(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException(nameof(name));

			return this._valuesByName?.TryGetValue(name);
		}

		public static bool IsValidName(string name)
		{
			return Asn1TypeDef.IsValidName(name);
		}
	}
}
