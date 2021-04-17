using System;
using System.Text;

namespace Products.Api.UnitTests.Builders
{
    public static class RandomBuilder
    {
        private static readonly Random Random = new Random();
            
        public static Guid NextGuid()
        {
            return Guid.NewGuid();
        }
       
        public static string NextString()
        {
            return NextGuid().ToString();
        }

        public static string NextString(int maxChars)
        {
            var nextString = new StringBuilder();

            while (nextString.Length < maxChars)
            {
                nextString.Append(NextString());
            }

            return nextString.ToString().Substring(0, Math.Min(maxChars, nextString.Length));
        }
    }
}