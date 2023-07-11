namespace Shezzy.Authentication.User
{
	public interface IUser
	{
		string? NameIdentifier { get; set; }
		string? Name { get; set; }
		string? GivenName { get; set; }
		string? Picture { get; set; }
		string? EmailAddress { get; set; }
		string? PhoneNumber { get; set; }
		string? Sub { get; set; }
		bool? EmailVerified { get; set; }
	}
}
