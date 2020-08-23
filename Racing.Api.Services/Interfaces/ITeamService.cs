using Racing.Model;
using System.Collections.Generic;

namespace Racing.Api.Services.Interfaces
{
    public interface ITeamService
    {
        bool CreateTeam(Team team);

        bool EditTeam(Team team);

        bool DeleteTeam(int id);

        IList<Team> GetTeams();
    }
}
