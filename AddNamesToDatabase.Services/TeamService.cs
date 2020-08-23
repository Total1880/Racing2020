using AddNamesToDatabase.Repositories;
using AddNamesToDatabase.Services.Interfaces;
using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddNamesToDatabase.Services
{
    public class TeamService : ITeamService
    {
        private readonly IRepository<Team> _teamRepository;

        public TeamService(IRepository<Team> teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public bool CreateTeam(Team team)
        {
            return _teamRepository.Create(team);
        }

        public bool DeleteTeam(int id)
        {
            return _teamRepository.Delete(id);
        }

        public bool EditTeam(Team team)
        {
            return _teamRepository.Update(team);
        }

        public async Task<IList<Team>> GetTeams()
        {
            return await _teamRepository.Get();
        }
    }
}
