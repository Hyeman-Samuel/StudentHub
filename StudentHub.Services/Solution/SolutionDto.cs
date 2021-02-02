using StudentHub.Services.Image;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace StudentHub.Services.Solution
{
    public class SolutionDto
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public string TimeAdded { get; set; } 
        [Required]
        public Guid QuestionId { get; set; }
        public string AuthorId { get; set; }

        public List<ImageDto> Images { get; set; }
        public SolutionDto()
        {
            this.Images = new List<ImageDto>();
        }
    }


    public class SolutionResponseDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string TimeAdded { get; set; }
        public string AuthorId { get; set; }
        public Guid? QuestionId { get; set; }
        public List<ImageResponseDto> Images { get; set; }

        public SolutionResponseDto()
        {
            this.Images = new List<ImageResponseDto>();
        }
    }

    public class SolutionEditDto
    {
        public string Message { get; set; }
    }

}
