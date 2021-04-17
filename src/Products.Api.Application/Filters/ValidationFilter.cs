using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Products.Api.Application.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validationErrors = new List<ValidationFailure>();

                foreach (KeyValuePair<string, ModelStateEntry> entry in context.ModelState)
                {
                    if (entry.Value != null && entry.Value.Errors.Any())
                    {
                        foreach (ModelError error in entry.Value.Errors.Where(x => !string.IsNullOrWhiteSpace(x.ErrorMessage)))
                        {
                            validationErrors.Add(new ValidationFailure(entry.Key, error.ErrorMessage));
                        }
                    }
                }

                context.Result = new BadRequestObjectResult(validationErrors);
            }

            base.OnActionExecuting(context);
        }
    }
}
