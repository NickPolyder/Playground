using DDD.Rehydrate.Domain;
using Microsoft.EntityFrameworkCore;

namespace DDD.Rehydrate.Repositories;

public class PersonRepository(RehydrateDbContext dbContext) : IPersonRepository
{
    public async Task<Person> FindByEmail(string email, CancellationToken cancellationToken = default)
    {
        var person = await dbContext.Set<Person>()
            .FirstOrDefaultAsync(x => x.Email == email);

        if(person == null)
        {
            throw new ArgumentException("Could not find a person with that e-mail", nameof(email));
        }

        /** I am thinking this could be a Cross Cutting Concern where we pick up all models that have the 
             * IRehydrate implemented and handle them (Maybe a decorator pattern where there would be a 
             * IRehydrateHandler<TParent, THydrator> where TParent : IRehydrate<THydrator>
             * PersonLicenseHydratorHandler: IRehydrateHandler<Person, License>
             **/ 
        if (person is IRehydrate<IEnumerable<License>> rehydrator)
        {
            // This would probably be a bit different I havent find the proper place for it. 
            // Might be a good discussion 
            var licenses = await dbContext.Set<License>()
                .Where(item => item.UserId == person.Id)
                .ToListAsync(cancellationToken: cancellationToken);

            // this would NOT work person.Rehydrate(licenses);
            // it is not public because of the Interface explicit implementation
            // IRehydrate<IEnumerable<License>>.Rehydrate(IEnumerable<License> entities)
            rehydrator.Rehydrate(licenses);
        }

        return person;
    }
}