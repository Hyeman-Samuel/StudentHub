using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace StudentHub.Services.Reply
{
    public class ReplyDto
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public string TimeAdded { get; set; }
        [Required]
        public Guid? CommentRepliedToId { get; set; }
        public string AuthorId { get; set; }
    }

    public class ReplyResponseDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string TimeAdded { get; set; }
        public string AuthorId { get; set; }
        public Guid? CommentRepliedToId { get; set; }
    }
}
