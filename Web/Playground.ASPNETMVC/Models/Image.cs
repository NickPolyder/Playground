using System.ComponentModel.DataAnnotations;

namespace Playground.ASPNETMVC.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        public byte[] Img { get; set; }
    }
}