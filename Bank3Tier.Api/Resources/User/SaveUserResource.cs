﻿using System;
namespace Bank3Tier.Api.Resources.User
{
    public class SaveUserResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
