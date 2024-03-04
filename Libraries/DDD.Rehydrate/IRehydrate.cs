using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Rehydrate
{
    public interface IRehydrate<in T>
    {
        void Rehydrate(T entity);
    }
}
