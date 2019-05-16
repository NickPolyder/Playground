using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Playground.ASPNETCORE.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        { }

        public MyDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=LaptopNick\\SQLEXPRESS;Initial Catalog=aspnetcore.mvc.playground;Persist Security Info=True;User ID=sqlUser;Password=sqluser1");
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Section> Sections { get; set; }


        public static void Seed(MyDbContext dbContext)
        {
            dbContext.Database.Migrate();
            if (dbContext.Departments.Any() || dbContext.Sections.Any()) return;

            var departments = new List<Department>();
            for (int i = 1; i < 10; i++)
            {
                departments.Add(new Department
                {
                    Name = $"Department {i}"

                });
            }

            dbContext.Departments.AddRange(departments);
            dbContext.SaveChanges();
            var sections = new List<Section>();
            var random = new Random();
            for (int j = 1; j < 10; j++)
            {
                var departmentId = random.Next(1, departments.Count - 1);
                sections.Add(new Section
                {
                    Name = $"Section {j}",
                    DepartmentId = departments[departmentId].Id
                });
            }

            dbContext.Sections.AddRange(sections);
            dbContext.SaveChanges();
        }
    }
}
