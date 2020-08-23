using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddNamesToDatabase.Services.Interfaces
{
    public interface ITeamService
    {
        bool CreateTeam(Team team);

        bool EditTeam(Team team);

        bool DeleteTeam(int id);

        Task<IList<Team>> GetTeams();
    }
}
