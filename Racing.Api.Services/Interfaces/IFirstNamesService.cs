using Racing.Model;
using System.Collections.Generic;

namespace Racing.Api.Services.Interfaces
{
    public interface IFirstNamesService
    {
        bool CreateNames(IList<FirstNames> firstNames);
    }
}
