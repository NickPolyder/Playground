using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Playground.NETFRAMEWORK.Models;

namespace Playground.NETFRAMEWORK.Tests
{
    public static class Linq
    {
        public static void OfTypeMagic()
        {
            Console.WriteLine("Adding Test data");

            var list = new List<object>();
            list.Add("Yes Sir");
            list.Add(15);
            list.Add(55.5);
            list.Add('A');
            list.Add(99.99f);
            list.Add('C');
            list.Add(646554);
            list.Add(System.Net.HttpStatusCode.OK);
            list.Add("No!");
            list.Add(new Recursive());
            list.Add(typeof(Location));
            list.Add(92.5);
            Console.WriteLine("Begin...\n\n");
            foreach (var item in list.OfType<float>())
            {
                Console.WriteLine(item);
            }


            Console.WriteLine("\n\nEnd......");
        }
    }
}
