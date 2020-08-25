using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IList<Team>> GenerateTeams(int numberOfPeople, int numberOfTeams);
    }
}
