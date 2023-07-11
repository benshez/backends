namespace Shezzy.Authentication.User
{
	public class UserDTO : IUser
	{
		private string? _nameIdentifier;
		public string? Name { get; set; }
		public string? GivenName { get; set; }
		public string? Picture { get; set; }
		public string? EmailAddress { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Sub { get; set; }
		public string? NameIdentifier
		{
			get
			{
				return _nameIdentifier;
			}

			set { 
				_nameIdentifier = value; 
			}
		}
		public bool? EmailVerified { get; set; }
	}
}
