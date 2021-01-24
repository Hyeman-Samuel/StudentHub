using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Domain.Common
{
    public class Entity
    {
        public Guid Id { get; set; }
        public bool IsSoftDelete { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
