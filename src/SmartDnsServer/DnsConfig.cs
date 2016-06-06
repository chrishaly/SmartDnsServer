using System.IO;
using System.Xml.Serialization;

namespace SmartDnsServer
{
	public class DnsConfig
	{
		public DnsClientConfig[] Clients { get; set; }
		public DnsClientMappingConfig[] Mappings { get; set; }

		public string ToXmlString()
		{
			var ser = new XmlSerializer(typeof(DnsConfig));
			using (var writer = new StringWriter())
			{
				ser.Serialize(writer, this);
				var xml = writer.ToString();
				return xml;
			}
		}

		public static DnsConfig LoadFromXmlFile(string configFile)
		{
			var ser = new XmlSerializer(typeof(DnsConfig));
			using (var stm = File.OpenRead(configFile))
			{
				var obj = ser.Deserialize(stm);
				return obj as DnsConfig;
			}
		}
	}
}
