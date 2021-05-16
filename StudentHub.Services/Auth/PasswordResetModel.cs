using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentHub.Services.Auth
{
    public class PasswordResetModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
                                                                          