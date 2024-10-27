using System.Linq;

namespace Playground.NETSTANDARD.Models
{
    public class Contact
    {
        public string FullName { get; set; }

        public string Phone { get; set; }

        public override string ToString()
        {
            var fullName = $"FullName: {FullName}";
            return $"Contact {string.Join("", Enumerable.Repeat("-", fullName.Length + 1))}\n" +
                   fullName + $"\nPhone: {Phone}";
        }
    }
}
