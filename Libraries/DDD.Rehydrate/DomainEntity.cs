using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Rehydrate
{
    /// <summary>
    /// The Base Entity
    /// </summary>
    public abstract class DomainEntity
    {
        public string Id { get; }

        protected DomainEntity(string id)
        {
            Id = id;
        }
    }
}
