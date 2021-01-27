using StudentHub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentHub.Domain
{
    public class Solution 
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
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<Reaction> Reactions { get; set; }
        public Solution()
        {
            this.Comments = new List<Comment>();
            this.Images = new List<Image>();
            this.Reactions = new List<Reaction>();
        }
    }
}
