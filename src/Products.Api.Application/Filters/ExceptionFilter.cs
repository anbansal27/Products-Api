using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Products.Api.Application.Exceptions;
using System;
using System.Threading.Tasks;

namespace Products.Api.Application.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger) => _logger = logger;

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            context.Result = context.Exception switch
            {
                DuplicateProductException ex => await GetExceptionResult(ex, StatusCodes.Status409Conflict),
                DuplicateProductOptionException ex => await GetExceptionResult(ex, StatusCodes.Status409Conflict),
                ProductNotFoundException ex => await GetExceptionResult(ex, StatusCodes.Status400BadRequest),
                ProductOptionNotFoundException ex => await GetExceptionResult(ex, StatusCodes.Status400BadRequest),
                _ => await GetExceptionResult(context.Exception, StatusCodes.Status500InternalServerError)
            };
        }

        private async Task<IActionResult> GetExceptionResult(Exception exception, int statusCode)
        {
            _logger.LogError(exception, exception.Message);

            var result = await Task.FromResult(new ObjectResult(exception.Message)
            {
                StatusCode = statusCode
            });

            return result;
        }
    }
}
