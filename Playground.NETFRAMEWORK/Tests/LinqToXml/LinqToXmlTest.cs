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
            var nameSpace = xDoc.Root.GetDefaultNamespace()?.NamespaceName;
            var body = xDoc.Descendants(XName.Get("Body", nameSpace));
            var productXName = XName.Get("Product", nameSpace);
            var descriptionXName = XName.Get("Description", nameSpace);
            var identificationXName = XName.Get("Identification", nameSpace);
            var id1XName = XName.Get("ID1", nameSpace);
            var id2XName = XName.Get("ID2", nameSpace);
            var typeXName = XName.Get("Type", nameSpace);
            var nameXName = XName.Get("Name", nameSpace);
            var query = from t in body
                        select new
                        {
                            ID1 = t.Element(productXName).Element(descriptionXName).Element(identificationXName).Element(id1XName)?.Value,
                            ID2 = t.Element(productXName).Element(descriptionXName).Element(identificationXName).Element(id2XName)?.Value,
                            Name = t.Element(productXName).Element(descriptionXName).Element(typeXName).Element(nameXName)?.Value
                        };

            foreach (var item in query)
            {
                WriteLine($"ID1: {item.ID1} | ID2: {item.ID2} | Name: {item.Name}");
            }

        }
    }
}