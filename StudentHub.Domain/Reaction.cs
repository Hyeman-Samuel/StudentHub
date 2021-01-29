using StudentHub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentHub.Domain
{
    public class Reaction
    {
        public Guid Id { get; set; }
        public bool IsSoftDelete { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }
        [Required]
        public Vote Vote { get; set; }
        public Guid? QuestionId { get; set; }
        public Question Question { get; set; }
        public Guid? SolutionId { get; set; }
        public Solution Solution { get; set; }
    }

   public enum Vote
    {
        [Description("up")]
        UP = 1,
        [Description("down")]
        Down = 2,

    }
}
