using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace V01_SignIn.Models
{
    public class ValidationError
    {
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Target { get; }

        public string Message { get; }

        public ValidationError(string target, string message)
        {
            Target = !string.IsNullOrEmpty(target) ? target : "general";
            Message = message;
        }
    }
}
