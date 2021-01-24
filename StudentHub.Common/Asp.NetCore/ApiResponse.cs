using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace StudentHub.Core.Asp.NetCore
{
    public class ApiResponse
    {
        public ApiResponseCode Code { get; set; }
        public string Description { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public bool HasErros => Errors.Any();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }


    }
        public class ApiResponse<T> : ApiResponse
        {
            public T Payload { get; set; }

            public int TotalCount { get; set; }

            public ApiResponse(T data = default, string message = "", ApiResponseCode codes = ApiResponseCode.OK, int? totalCount = 0, string[] errors = default)
            {
                Payload = data;
                Description = message;
                Errors = errors.ToList();
                Code = !errors.Any() ? codes : codes == ApiResponseCode.OK ? ApiResponseCode.ERROR : codes;
                TotalCount = totalCount ?? 0;
            }
        }


    public enum ApiResponseCode
    {
        [Description("Success")]
        OK = 0,
        [Description("Not Found")]
        NOTFOUND = 1,
        [Description("Invalid Request")]
        INVALID_REQUEST = 2,
        [Description("Error")]
        ERROR = 3
    }
}

