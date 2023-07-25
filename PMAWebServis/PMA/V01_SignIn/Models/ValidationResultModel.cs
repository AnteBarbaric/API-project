using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace V01_SignIn.Models
{
    public class ValidationResultModel
    {
        public string Message { get; }

        public List<ValidationError> Errors { get; }

        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Message = "Error";
            Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
    }
}
