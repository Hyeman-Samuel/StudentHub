using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentHub.Services.Image
{
    public class ImageDto
    {
        [Required]
        public string base64Format { get; set; }
    }

    public class ImageResponseDto
    {
        public string Url { get; set; }
    }
}
