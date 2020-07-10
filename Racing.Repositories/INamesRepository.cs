using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.Repositories
{
    public interface INamesRepository<T>
    {
        Task<IList<T>> GenerateNames(int numberOfPeople);
    }
}
