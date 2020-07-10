using Racing.Model;
using System.Collections.Generic;

namespace AddNamesToDatabase.Services.Interfaces
{
    public interface INameService
    {
        bool CreateNames(IList<FirstNames> firstNames, IList<LastNames> lastNames);
    }
}
