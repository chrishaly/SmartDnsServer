# SmartDnsServer

this is just a DNS Proxy Server.

your can configurate rules to decide which DNS Server to resolve a domain by Regular Expression.

```
<?xml version="1.0" encoding="utf-16"?>
<DnsConfig xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
	<Clients>
		<DnsClientConfig>
			<Id>ns1</Id>
			<IPAddresses>
				<string>192.168.102.20</string>
			</IPAddresses>
			<QueryTimeout>30000</QueryTimeout>
		</DnsClientConfig>
		<DnsClientConfig>
			<Id>default</Id>
			<IPAddresses>
				<string>8.8.8.8</string>
				<string>8.8.4.4</string>
			</IPAddresses>
			<QueryTimeout>30000</QueryTimeout>
		</DnsClientConfig>
	</Clients>
	<Mappings>
		<DnsClientMappingConfig>
			<Match>.*\.baidu\.com\.</Match>
			<ClientId>ns1</ClientId>
		</DnsClientMappingConfig>
		<DnsClientMappingConfig>
			<Match>.*\.google\.com\.</Match>
			<ClientId>ns1</ClientId>
		</DnsClientMappingConfig>
	</Mappings>
</DnsConfig>
```
