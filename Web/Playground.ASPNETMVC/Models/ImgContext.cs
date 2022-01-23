using System.Data.Entity;

namespace Playground.ASPNETMVC.Models
{
    public class ImgContext : DbContext
    {
        public ImgContext() : base("Data Source=DeskNick\\SQLEXPRESS;Initial Catalog=aspnet.mvc.saveImage;Persist Security Info=True;User ID=sqlUser;Password=sqluser1")
        {

        }

        public DbSet<Image> Images { get; set; }
    }
}