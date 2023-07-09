namespace Shezzy.Authentication.Services
{
	public interface IUser
	{
		public string? Name { get; set; }
		public string? GivenName { get; set; }
		public string? Picture { get; set; }
		public string? EmailAddress { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Sub { get; set; }
		public bool? EmailVerified { get; set; }
	}
}
