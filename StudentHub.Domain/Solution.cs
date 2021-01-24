using StudentHub.Domain.Common;
using StudentHub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentHub.Domain
{
    public class Solution : Post
    {
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
