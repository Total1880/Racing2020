using Racing.Model;
using System.Collections.Generic;

namespace Racing.Api.Services.Interfaces
{
    public interface INationService
    {
        bool CreateNation(Nation nation);

        bool EditNation(Nation nation);

        bool DeleteNation(int id);

        IList<Nation> GetNations();
    }
}
