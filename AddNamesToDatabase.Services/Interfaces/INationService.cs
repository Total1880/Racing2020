using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddNamesToDatabase.Services.Interfaces
{
    public interface INationService
    {
        bool CreateNation(Nation nation);

        bool EditNation(Nation nation);

        bool DeleteNation(int id);

        Task<IList<Nation>> GetNations();
    }
}
