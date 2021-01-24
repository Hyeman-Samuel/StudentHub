using StudentHub.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Domain.Identity
{
    public class RefreshToken : Entity
    {
        public string Token { get; set; }
        public string JwtId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsUsed { get; set; }
        public bool Invalidated { get; set; }
        public string UserId { get; set; }
    }
}
