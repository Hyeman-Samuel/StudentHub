using StudentHub.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentHub.Domain
{
   public class Image: Entity
    {
        public string Latex { get; set; }
        [Required]
        public string ImageLink { get; set; }

        public Guid? QuestionId { get; set; }
        public Question Question { get; set; }
        public Guid? SolutionId { get; set; }
        public Solution Solution { get; set; }
    }
}
