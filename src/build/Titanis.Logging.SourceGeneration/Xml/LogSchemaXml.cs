using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Titanis.Logging.SourceGeneration.Xml
{
	[XmlRoot(ElementName = "logSchema", Namespace = LogSchemaNamespace)]
	public class LogSchemaXml
	{
		public const string LogSchemaNamespace = "xsd://titanis/LogSchema.xsd";

		[XmlElement("source")]
		public SourceXml[] Sources { get; set; }
	}

	public class SourceXml
	{
		[XmlAttribute("ns")]
		public string? Namespace { get; set; }

		[XmlAttribute("name")]
		public string? Name { get; set; }

		[XmlElement("message")]
		public MessageXml[]? Messages { get; set; }

		[XmlElement("using")]
		public UsingXml[]? Imports { get; set; }
	}

	public class UsingXml
	{
		[XmlAttribute("ns")]
		public string Namespace { get; set; }
	}

	public class MessageXml
	{
		[XmlAttribute("name")]
		public string? Name { get; set; }

		[XmlAttribute("severity")]
		public string? Severity { get; set; }

		[XmlElement("param")]
		public ParameterXml[]? Parameters { get; set; }

		[XmlText]
		public string? Format { get; set; }
	}

	public class ParameterXml
	{
		[XmlAttribute("name")]
		public string? Name { get; set; }

		[XmlAttribute("type")]
		public string? Type { get; set; }
	}
}
