using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Playground.NETCORE.Tests.DI
{
    public class DITests : ITestCase
    {
        public string Name { get; } = "Simple DI Tests";
        public void Run()
        {
            SimpleDI dI = new SimpleDI();
            dI.AddPerUse<EnviromentTests, EnviromentTests>();
            dI.AddSingleton<StringBuilder, StringBuilder>();
            dI.AddPerUse<KindOfString, KindOfString>();
            var kindOf = dI.GetService<KindOfString>();
            kindOf.Write("Geia \n");
            kindOf.Write("Me \n");
            var kindOf2 = dI.GetService<KindOfString>();
            kindOf2.Write("Lene \n");
            kindOf2.Write("Niko \n");

            var builder = dI.GetService<StringBuilder>();
            Console.WriteLine(builder.ToString());
        }
    }
}
