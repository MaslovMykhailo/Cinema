﻿using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;

namespace Cinema.Persisted.Entities
{
    public class AuthUser : IdentityUser
    {
        public string Token { get; set; }
        public string Role { get; set; }
    }

}
