using Racing.Model;
using System.Collections.Generic;

namespace Racing.Api.Services.Interfaces
{
    public interface ILastNamesService
    {
        bool CreateNames(IList<LastNames> lastNames);

        IList<LastNames> GenerateLastNames(int numberOfNames);
    }
}
