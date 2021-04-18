using System;

namespace Products.Api.IntegrationTests.Common
{
    public static class RandomBuilder
    {
        private static readonly Random Random = new Random();

        public static decimal NextDecimal()
        {
            return (decimal)NextDouble();
        }

        public static Guid NextGuid()
        {
            return Guid.NewGuid();
        }

        public static string NextString()
        {
            return NextGuid().ToString();
        }

        private static double NextDouble(int fractionalPlaces = 2)
        {
            lock (Random)
            {
                return Math.Round(Random.NextDouble(), fractionalPlaces);
            }
        }
    }
}