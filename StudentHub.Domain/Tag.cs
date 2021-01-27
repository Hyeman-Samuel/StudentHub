using StudentHub.Domain.Join;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentHub.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }
        public bool IsSoftDelete { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public string Title { get; set; }
        public IEnumerable<QuestionTag> Questions { get; set; }
        public TagType Type { get; set; }
    }

    public enum TagType
    {
        OFFICIAL,
        CUSTOM
    }
}
