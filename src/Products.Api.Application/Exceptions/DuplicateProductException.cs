using System;

namespace Products.Api.Application.Exceptions
{
    public class DuplicateProductException : Exception
    {
        public DuplicateProductException(string message) : base(message)
        {
        }
    }
}