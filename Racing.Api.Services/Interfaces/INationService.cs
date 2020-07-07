using Racing.Model;
using System.Collections.Generic;

namespace Racing.Api.Services.Interfaces
{
    public interface INationService
    {
        bool CreateNation(Nation nation);

        IList<Nation> GetNations();
    }
}
