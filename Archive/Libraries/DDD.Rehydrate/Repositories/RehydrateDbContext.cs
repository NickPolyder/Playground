using DDD.Rehydrate.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Rehydrate.Repositories
{
    public class RehydrateDbContext : DbContext
    {
        public RehydrateDbContext(DbContextOptions<RehydrateDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Not a full configuration
            modelBuilder.Entity<Person>()
                .HasMany<License>();
        }
    }
}
