using Microsoft.AspNetCore.Identity;
using StudentHub.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<Reaction> ReactionsMade { get; set; }
        public IEnumerable<Reply> RepliesMade { get; set; }
        public IEnumerable<Comment> CommentsMade { get; set; }
        public IEnumerable<Solution> SolutionsMade { get; set; }
        public IEnumerable<Question> QuestionsPosted { get; set; }

        public int Points { get; set; }
    }
}
