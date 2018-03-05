using System.ComponentModel.DataAnnotations;

namespace Playground.ASPNETCORE.Models
{

    public class Section
    {
        [Key]
        public int Id { get; set; }

        public int DepartmentId { get; set; }

        public string Name { get; set; }

    }
}