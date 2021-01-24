using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace StudentHub.Services.Comment
{
    public class CommentDto
    {
        [Required]
        public string Message { get; set; }        
        [Required]
        public string TimeAdded { get; set; }
        [Required]
        public Guid QuestionId { get; set; }
        public string AuthorId { get; set; }
    }

    public class CommentResponseDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string TimeAdded { get; set; }
        public string AuthorId { get; set; }
        public Guid QuestionId { get; set; }
    }
}
