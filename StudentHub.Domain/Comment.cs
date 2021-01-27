using Microsoft.Extensions.DependencyInjection;
using StudentHub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentHub.Domain
{
    public class Comment
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

        public Guid? QuestionId { get; set; }
        public Question Question { get; set; }
        public Guid? SolutionId { get; set; }
        public Solution Solution{ get; set; }
        public IEnumerable<Reply> Replies { get; set; }

        public Comment()
        {
            this.Replies = new List<Reply>();
        }
    }
}
