using StudentHub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudentHub.Domain
{
    public class Reply
    {
        public Guid Id { get; set; }
        public bool IsSoftDelete { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public string Message { get; set; }
        [Required]
        public string TimeAdded { get; set; }
        [Required]
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        public Guid? CommentRepliedToId { get; set; }
        public Comment CommentRepliedTo { get; set; }
    }
}
