using System.Collections.Generic;
using Playground.NETFRAMEWORK.Models;

namespace Playground.NETFRAMEWORK.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Playground.NETFRAMEWORK.Models.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Playground.NETFRAMEWORK.Models.BlogContext context)
        {
            if (!context.Blogs.Any())
            {
                var list = new List<Blog>();
                for (int i = 0; i < 10; i++)
                {
                    list.Add(new Blog
                    {
                        Author = i % 2 == 0 ? "Nick" : "Kitsos",
                        Text = "Text Info " + i,
                        Title = "Title " + i
                    });
                }
                context.Blogs.AddRange(list);
                context.SaveChanges();
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
