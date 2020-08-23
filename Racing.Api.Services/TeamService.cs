using Racing.Api.Repositories;
using Racing.Api.Services.Interfaces;
using Racing.Model;
using System.Collections.Generic;

namespace Racing.Api.Services
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

        public IList<Team> GetTeams()
        {
            return _teamRepository.Get();
        }
    }
}
