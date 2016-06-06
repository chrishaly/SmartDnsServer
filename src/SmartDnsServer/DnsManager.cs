using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ARSoft.Tools.Net.Dns;

namespace SmartDnsServer
{
	internal class DnsManager
	{
		private readonly DnsClient _defaultDnsClient;
		private readonly Dictionary<string, DnsClient> _domainToDnsClient = new Dictionary<string, DnsClient>();

		public DnsManager(DnsConfig config)
		{
			if (config == null)
				throw new ArgumentNullException(@"config");

			var clients = new Dictionary<string, DnsClient>();
			foreach (var it in config.Clients)
			{
				var client = new DnsClient(it.IPAddresses.Select(ip => IPAddress.Parse(ip)), it.QueryTimeout);
				if ("default".Equals(it.Id, StringComparison.OrdinalIgnoreCase))
					_defaultDnsClient = client;
				clients[it.Id] = client;
			}

			foreach (var item in config.Mappings)
			{
				_domainToDnsClient[item.Match] = clients.TryGetValue(item.ClientId);
			}
		}

		private DnsClient GetDnsClient(string domain)
		{
			foreach (var item in _domainToDnsClient)
			{
				try
				{
					if (Regex.Match(domain, item.Key).Success)
					{
						return item.Value;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
			}

			return _defaultDnsClient;
		}

		public async Task<DnsMessage> ResolveAsync(DnsQuestion question)
		{
			var client = GetDnsClient(question.Name.ToString());

			var dnsMessage = await client.ResolveAsync(question.Name, question.RecordType);
			return dnsMessage;
		}
	}

	public static class DictionaryExtensionFunc
	{
		public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> src, TKey key)
		{
			TValue value;
			src.TryGetValue(key, out value);
			return value;
		}
	}
}