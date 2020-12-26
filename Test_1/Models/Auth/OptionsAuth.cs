using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Auth
{
    public class OptionsAuth
    {
        public const string ISSUER = "Server"; 
        public const string AUDIENCE = "Client"; 
        const string KEY = "any_key";   
        public const int LIFETIME = 5; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
