using AddNamesToDatabase.Repositories;
using Racing.Model;
using Racing.Model.Enums;
using System.Collections.Generic;
using System.Linq;
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
            CheckRacePoints(race);
            CheckRaceParts(race);

            return _raceRepository.Create(race);
        }

        public bool DeleteRace(int id)
        {
            return _raceRepository.Delete(id);
        }

        public bool EditRace(Race race)
        {
            CheckRaceParts(race);
            CheckRacePoints(race);

            return _raceRepository.Update(race);
        }

        public Task<IList<Race>> GetRaces()
        {
            return _raceRepository.Get();
        }

        private void CheckRaceParts(Race race)
        {
            if (race.RacePartList == null || race.RacePartList.Count == 0)
            {
                var newRacePart = new RacePart();

                newRacePart.Start = 1;
                newRacePart.End = race.Length;
                newRacePart.Part = RacePartEnum.Flat;

                race.RacePartList = new List<RacePart>();
                race.RacePartList.Add(newRacePart);
            }
        }

        private void CheckRacePoints(Race race)
        {
            foreach (var racePoint in race.RacePointList)
            {
                if (racePoint.Position == 1 || racePoint.Point == 0)
                {
                    continue;
                }

                var temp = race.RacePointList.Where(p => p.Position == racePoint.Position - 1).FirstOrDefault().Point;
                if (racePoint.Point >= temp)
                {
                    racePoint.Point = race.RacePointList.Where(p => p.Position == racePoint.Position - 1).FirstOrDefault().Point - 1;
                }

                if (racePoint.Point < 0)
                {
                    racePoint.Point = 0;
                }
            }
        }
    }
}
