namespace SmartDnsServer
{
	public class DnsClientConfig
	{
		/// <summary>
		/// id
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// supper dns server IP Addresses
		/// </summary>
		public string[] IPAddresses { get; set; }

		/// <summary>
		/// in milliseconds
		/// </summary>
		public int QueryTimeout { get; set; }
	}
}