using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using ARSoft.Tools.Net.Dns;

namespace SmartDnsServer
{
	class DnsServerManager
	{
		private DnsManager _dnsManager;
		private DnsServer _dnsServer;

		public void Start()
		{
			var configFile = Assembly.GetEntryAssembly().Location;
			configFile = Path.Combine(Path.GetDirectoryName(configFile), "dns.config.xml");
			var config = DnsConfig.LoadFromXmlFile(configFile);

			_dnsManager = new DnsManager(config);

			_dnsServer = new DnsServer(10, 10);
			_dnsServer.QueryReceived += OnQueryReceived;
			_dnsServer.Start();
		}

		private async Task OnQueryReceived(object sender, QueryReceivedEventArgs e)
		{
			var query = e.Query as DnsMessage;

			if (query == null)
				return;

			var response = query.CreateResponseInstance();

			foreach (var question in query.Questions)
			{
				var dnsMessage = await _dnsManager.ResolveAsync(question);
				if ((dnsMessage == null) || ((dnsMessage.ReturnCode != ReturnCode.NoError) && (dnsMessage.ReturnCode != ReturnCode.NxDomain)))
				{
					response.ReturnCode = ReturnCode.ServerFailure;
					return;
				}

				foreach (var dnsRecord in dnsMessage.AnswerRecords)
				{
					response.AnswerRecords.Add(dnsRecord);
				}

				response.AuthorityRecords.AddRange(dnsMessage.AuthorityRecords);
			}
			//var domain = query.Questions[0].Name.ToString();
			//var dnsMessage = DnsClient.Default.Resolve(DomainName.Parse(domain), RecordType.A);

			//response.ReturnCode = ReturnCode.ServerFailure;

			e.Response = response;
		}
	}
}
