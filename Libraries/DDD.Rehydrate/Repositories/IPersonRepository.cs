using DDD.Rehydrate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Rehydrate.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> FindByEmail(string email, CancellationToken cancellationToken = default);
    }
}
