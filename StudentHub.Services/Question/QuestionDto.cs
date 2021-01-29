using StudentHub.Domain.Identity;
using StudentHub.Services.Image;
using StudentHub.Services.Tag;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace StudentHub.Services.Question
{
    public class QuestionDto
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public string Title { get; set; }        
        public string AuthorId { get; set; } 
        [Required]
        public string TimeAdded { get; set; }

        public List<Guid> TagIds { get; set; }
        public List<ImageDto> Images { get; set; }
        public QuestionDto()
        {
            this.Images = new List<ImageDto>();
            this.TagIds = new List<Guid>();
        }
    }

    public class QuestionEditDto
    {
        public string Message { get; set; }
        public string Title { get; set; }
    }


        public class QuestionResponseDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string TimeAdded { get; set; }
        public string AuthorId { get; set; }
        public List<ImageResponseDto> Images { get; set; }
        public List<TagResponseDto> TagResponseDtos { get; set; }

        public QuestionResponseDto()
        {
            this.Images = new List<ImageResponseDto>();
            this.TagResponseDtos = new List<TagResponseDto>();
        }

    }


}
