using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Services.Auth
{
    public class NewPasswordModel
    {
        public string NewPassword { get; set; }
        public string NewPasswordConfirmation { get; set; }
    }
}
