using StudentHub.Domain.Common;
using StudentHub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Domain
{
    public class Question : Post
    {
        public string Title { get; set; } 

        public IEnumerable<Image> Images {get; set;}  
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Solution> Solutions { get; set; }        
        public IEnumerable<Reaction> Reactions { get; set; }

        public Question()
        {
            this.Images = new List<Image>();
            this.Comments = new List<Comment>();
            this.Solutions = new List<Solution>();
            this.Reactions = new List<Reaction>();
        }
    }
}
