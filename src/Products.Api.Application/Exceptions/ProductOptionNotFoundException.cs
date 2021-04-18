using System;

namespace Products.Api.Application.Exceptions
{
    public class ProductOptionNotFoundException : Exception
    {
        public ProductOptionNotFoundException(string message) : base(message)
        {
        }
    }
}