using System.ComponentModel.DataAnnotations;

namespace Playground.ASPNETCORE.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }

}