﻿using System;
namespace Bank3Tier.Api.Resources.Auth
{
    public class LoginUserResponseResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
