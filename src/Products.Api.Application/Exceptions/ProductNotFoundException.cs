using System;

namespace Products.Api.Application.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message) : base(message)
        {
        }
    }
}