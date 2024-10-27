using System;
using System.Collections.Generic;
using System.Linq;
using Playground.NETFRAMEWORK.Models;

namespace Playground.NETFRAMEWORK.Tests
{
    public static class Linq
    {
        public static void OfTypeMagic()
        {
            Console.WriteLine("Adding Test data");

            var list = new List<object>
            {
                "Yes Sir",
                15,
                55.5,
                'A',
                99.99f,
                'C',
                646554,
                System.Net.HttpStatusCode.OK,
                "No!",
                new Recursive(),
                typeof(Location),
                92.5
            };
            Console.WriteLine("Begin...\n\n");
            foreach (var item in list.OfType<float>())
            {
                Console.WriteLine(item);
            }


            Console.WriteLine("\n\nEnd......");
        }
    }
}
