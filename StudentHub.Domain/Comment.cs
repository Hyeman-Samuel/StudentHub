using Microsoft.Extensions.DependencyInjection;
using StudentHub.Domain.Common;
using StudentHub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Domain
{
    public class Comment : Post
    {
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
