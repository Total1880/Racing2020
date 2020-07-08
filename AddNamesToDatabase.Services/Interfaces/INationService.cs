using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddNamesToDatabase.Services.Interfaces
{
    public interface INationService
    {
        bool CreateNation(Nation nation);

        Task<IList<Nation>> GetNations();
    }
}
