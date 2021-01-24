using StudentHub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Domain.Common
{
    public class Reply : Post
    {
        public Guid CommentRepliedToId { get; set; }
        public Comment CommentRepliedTo { get; set; }
    }
}
