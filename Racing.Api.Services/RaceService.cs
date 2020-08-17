using Racing.Api.Repositories;
using Racing.Api.Services.Interfaces;
using Racing.Model;
using System.Collections.Generic;

namespace Racing.Api.Services
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

        public IList<Race> GetRaces()
        {
            return _raceRepository.Get();
        }
    }
}
