﻿using System.Collections.Generic;
using System.Security.Claims;

namespace Shezzy.Authentication.User
{
    public class UserResponseDTO
    {
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string Picture { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Sub { get; set; }
        public string NameIdentifier { get; set; }
        public string Audience { get; set; }
        public object AccessToken { get; set; }
        public bool EmailVerified { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}
