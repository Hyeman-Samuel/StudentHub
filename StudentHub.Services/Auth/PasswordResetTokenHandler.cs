using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Services.Auth
{
    public class PasswordResetTokenHandler
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
