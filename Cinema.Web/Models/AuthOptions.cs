using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Cinema.Web.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "CinemaServer";
        public const string AUDIENCE = "https://localhost:44321";
        const string KEY = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS";
        public const int LIFETIME = 5;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            byte[] bytes = Encoding.UTF8.GetBytes(KEY);
            var secret = Convert.ToBase64String(bytes);

            return new SymmetricSecurityKey(Encoding.Default.GetBytes(secret));
        }
    }
}
