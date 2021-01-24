using StudentHub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentHub.Domain.Common
{
    public class Post :Entity
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public string TimeAdded { get; set; }
        [Required]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }
    }
}
