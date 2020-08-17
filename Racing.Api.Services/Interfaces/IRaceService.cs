using Racing.Model;
using System.Collections.Generic;

namespace Racing.Api.Services.Interfaces
{
    public interface IRaceService
    {
        bool CreateRace(Race race);

        bool EditRace(Race race);

        bool DeleteRace(int id);

        IList<Race> GetRaces();
    }
}
