using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Services.Reaction
{
    public class ReactionDto
    {
        public Guid? QuestionId { get; set; }
        public Guid? SolutionId { get; set; }
        public string AuthorId { get; set; }
    }
}
