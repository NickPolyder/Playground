using System.Linq;
using System.Xml;
using System.Xml.Linq;
using static System.Console;
namespace Playground.NETFRAMEWORK.Tests.LinqToXml
{
    public class LinqToXmlTest
    {
        private const string FileName = ".\\Tests\\LinqToXml\\Example.xml";

        public static void Run()
        {
            WriteLine("Linq To XML Test");

            var xml = XmlReader.Create(FileName);
            var xDoc = XDocument.Load(xml);
            var body = xDoc.Descendants("Body");
            var descendants = xDoc.Descendants();
            var query = from t in body
                        select new
                        {
                            ID1 = t.Element("Product").Element("Description").Element("Identification").Element("ID1")?.Value,
                            ID2 = t.Element("Product").Element("Description").Element("Identification").Element("ID2")?.Value,
                            Name = t.Element("Product").Element("Description").Element("Type").Element("Name").Value
                        };

            foreach (var item in query)
            {
                WriteLine($"ID1: {item.ID1} | ID2: {item.ID2} | Name: {item.Name}");
            }

        }
    }
}