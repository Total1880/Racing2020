using Racing.Model;
using Racing.Repositories;
using Racing.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.Services
{
    class RaceService : IRaceService
    {
        private IRepository<Race> _raceRepository;

        public RaceService(IRepository<Race> raceRepository)
        {
            _raceRepository = raceRepository;
        }
        public async Task<IList<Race>> GetRaces()
        {
            return await _raceRepository.Get();
        }
    }
}
