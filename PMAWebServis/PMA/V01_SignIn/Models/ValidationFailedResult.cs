using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace V01_SignIn.Models
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            :this(modelState, StatusCodes.Status422UnprocessableEntity)
        { }

        public ValidationFailedResult(ModelStateDictionary modelState, int statusCode)
        : base(new ValidationResultModel(modelState))
        {
            StatusCode = statusCode;
        }
    }
}
