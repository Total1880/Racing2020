using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.Repositories
{
    public interface IRepository<T>
    {
        Task<IList<T>> Get();
    }
}
