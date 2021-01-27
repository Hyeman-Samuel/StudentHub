using StudentHub.Domain.Identity;
using StudentHub.Domain.Join;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentHub.Domain
{
    public class Question
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
        public string Title { get; set; } 

        public IEnumerable<Image> Images {get; set;}  
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Solution> Solutions { get; set; }        
        public IEnumerable<Reaction> Reactions { get; set; }
        public IEnumerable<QuestionTag> Questions { get; set; }
        public Question()
        {
            this.Images = new List<Image>();
            this.Comments = new List<Comment>();
            this.Solutions = new List<Solution>();
            this.Reactions = new List<Reaction>();
            this.Questions = new List<QuestionTag>();
        }
    }
}
