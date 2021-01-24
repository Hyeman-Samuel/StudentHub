using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Services.Auth
{
    public class AuthResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string ExpiryTimeinMinutes { get; set; }
    }
}
