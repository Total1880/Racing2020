using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddNamesToDatabase.Services.Interfaces
{
    public interface IRaceService
    {
        bool CreateRace(Race race);

        bool EditRace(Race race);

        bool DeleteRace(int id);

        Task<IList<Race>> GetRace();
    }
}
