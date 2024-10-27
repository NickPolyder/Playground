using System.Threading;
using System.Threading.Tasks;

namespace Playground.NETFRAMEWORK.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BlogContext : DbContext
    {
        // Your context has been configured to use a 'DbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Playground.NETFRAMEWORK.Models.DbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DbContext' 
        // connection string in the application configuration file.
        public BlogContext()
            : base("name=DbContext")
        {
        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Location> Locations { get; set; }

        public override int SaveChanges()
        {
            UpdateContext();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            UpdateContext();
            return base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            UpdateContext();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateContext()
        {
            var trackableEntries = ChangeTracker.Entries()
                .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified)
                    && x.Entity.GetType().IsSubclassOf(typeof(BaseModel))); //Updated Created and Updated properties only for the entities that inherit from CmlBaseModel
            foreach (var trackableEntry in trackableEntries)
            {
                if (trackableEntry.State == EntityState.Added)
                {
                    trackableEntry.Property("CreatedAt").CurrentValue = DateTime.Now;
                    trackableEntry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                }

                if (trackableEntry.State == EntityState.Modified)
                {
                    trackableEntry.Property("CreatedAt").IsModified = false;
                    trackableEntry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                }
            }
        }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}