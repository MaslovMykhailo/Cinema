﻿using System.ComponentModel.DataAnnotations;

namespace Cinema.Web.Models
{
    public class SignInModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
