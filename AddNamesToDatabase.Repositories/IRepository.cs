using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.Api.Repositories
{
    public interface IRepository<T>
    {
        bool Create(T item);
        Task<IList<T>> Get();
        bool Update(T item);
        bool Delete(int id);
    }
}
