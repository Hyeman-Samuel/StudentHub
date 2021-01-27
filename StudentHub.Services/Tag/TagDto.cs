using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Services.Tag
{
    public class TagDto
    {
        public string Title { get; set; }
        public string AuthorId { get; set; }
    }


    public class TagResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
