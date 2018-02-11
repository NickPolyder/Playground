using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Playground.NETCORE.Models;

namespace Playground.NETCORE
{
    public class DbCoreContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKNICK\\SQLEXPRESS;Database=Playground.EFCore;");
            optionsBuilder.UseLoggerFactory(new Logger.LoggerFactory());

        }

        public static async Task Seed(DbCoreContext db)
        {
            var hasUsers = await db.Users.AnyAsync();
            if (!hasUsers)
            {

                db.Users.AddRange(User.Create("NickPol", "nick@mail.com"),
                                  User.Create("Kostas", "kostas@mail.com"),
                                  User.Create("George", "george@mail.com"),
                                  User.Create("Panos", "panos@mail.com"),
                                  User.Create("Mitsos", "mitsos@mail.com"));

                await db.SaveChangesAsync();

            }
        }
    }
}
