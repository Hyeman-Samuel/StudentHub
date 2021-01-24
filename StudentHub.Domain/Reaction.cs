﻿using StudentHub.Domain.Common;
using StudentHub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentHub.Domain
{
    public class Reaction : Entity
    {   [Required]
        public ApplicationUser Responder { get; set; }
        [Required]
        public string Vote { get; set; }
        public Guid? QuestionId { get; set; }
        public Question Question { get; set; }
        public Guid? SolutionId { get; set; }
        public Solution Solution { get; set; }
    }

   public enum Vote
    {
        [Description("up")]
        UP = 1,
        [Description("down")]
        Down = 2,

    }
}
