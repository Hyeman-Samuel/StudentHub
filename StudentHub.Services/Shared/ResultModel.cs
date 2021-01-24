using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace StudentHub.Services.ResultModel
{
    public class ResultModel<T>
    {
        private readonly List<ValidationResult> errors = new List<ValidationResult>();

        public T Data { get; set; }

        public void AddError(string error) => errors.Add(new ValidationResult(error));
        public bool HasError
        {
            get { return errors.Any(); }
        }

        public string[] Errors => errors.Select(c => c.ErrorMessage).ToArray();
    }
}
