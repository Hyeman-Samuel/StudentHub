using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Domain.Join
{
    public class QuestionTag
    {
        public Guid Id { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
