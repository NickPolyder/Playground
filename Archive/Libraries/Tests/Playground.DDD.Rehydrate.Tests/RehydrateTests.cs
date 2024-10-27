using DDD.Rehydrate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Playground.DDD.Rehydrate.Tests
{
    public class RehydrateTests
    {
        [Fact]
        public async Task RehydrateUseCase()
        {
            using(var context = new RehydrateDbContext(new DbContextOptions<RehydrateDbContext>()))
            {
                var personRepo = new PersonRepository(context);

                var person = await personRepo.FindByEmail("nick@email.com");

                Assert.NotNull(person.Licenses);
            }
        }
    }
}