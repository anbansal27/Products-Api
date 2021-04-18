using System;

namespace Products.Api.Application.Exceptions
{
    public class DuplicateProductOptionException : Exception
    {
        public DuplicateProductOptionException(string message) : base(message)
        {
        }
    }
}