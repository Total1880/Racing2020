using System.Collections.Generic;

namespace Racing.Api.Repositories
{
    public interface INamesRepository<T>
    {
        bool CreateNames(IList<T> names);

        IList<T> GenerateNames(int numberOfNames);
    }
}
