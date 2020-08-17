using Racing.Api.Repositories;
using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddNamesToDatabase.Services.Interfaces
{
    public class RaceService : IRaceService
    {
        private readonly IRepository<Race> _raceRepository;

        public RaceService(IRepository<Race> raceRepository)
        {
            _raceRepository = raceRepository;
        }

        public bool CreateRace(Race race)
        {
            return _raceRepository.Create(race);
        }

        public bool DeleteRace(int id)
        {
            return _raceRepository.Delete(id);
        }

        public bool EditRace(Race race)
        {
            return _raceRepository.Update(race);
        }

        public Task<IList<Race>> GetRaces()
        {
            return _raceRepository.Get();
        }
    }
}
