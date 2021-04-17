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
            switch (context.Exception)
            {
                case DuplicateProductException ex:
                    LogException(ex);
                    context.Result = new ConflictObjectResult(ex.Message);
                    await Task.CompletedTask;
                    break;
                case ProductNotFoundException ex:
                    LogException(ex);
                    context.Result = new NotFoundObjectResult(ex.Message);
                    await Task.CompletedTask;
                    break;
                default:
                    LogException(context.Exception);
                    context.Result = new ObjectResult("An unexpected error was encountered")
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                    await Task.CompletedTask;
                    break;
            }
        }

        private void LogException(Exception exception)
        {
            _logger.LogError(exception, exception.Message);
        }
    }
}
