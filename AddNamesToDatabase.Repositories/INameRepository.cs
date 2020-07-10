using System.Collections.Generic;

namespace AddNamesToDatabase.Repositories
{
    public interface INameRepository<T>
    {
        bool CreateNames(IList<T> names);
    }
}
