using Racing.Repositories;
using Racing.Model;
using Racing.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Racing.Services
{
    public class RacerPersonService : IRacerPersonService
    {
        private INamesRepository<FirstNames> _firstNamesRepository;
        private INamesRepository<LastNames> _lastNamesRepository;
        private IRepository<Team> _teamRepository;
        private readonly Random _random = new Random();
        private int _minAge = 16;
        private int _maxAge = 40;

        public RacerPersonService(INamesRepository<FirstNames> firstNamesRepository, INamesRepository<LastNames> lastNamesRepository, IRepository<Team> teamRepository)
        {
            _firstNamesRepository = firstNamesRepository;
            _lastNamesRepository = lastNamesRepository;
            _teamRepository = teamRepository;
        }

        public async Task<IList<RacerPerson>> GenerateRacerPeople(int numberOfPeople, int numberOfTeams)
        {
            while (numberOfPeople % numberOfTeams != 0)
            {
                numberOfPeople--;
            }

            var firstNames = await _firstNamesRepository.GenerateNames(numberOfPeople);
            var lastNames = await _lastNamesRepository.GenerateNames(numberOfPeople);
            var teams = await _teamRepository.Get();
            var generatedRacerPeople = new List<RacerPerson>();
            int teamIndex = 0;
            int index = 0;

            foreach (var name in firstNames)
            {
                RacerPerson newRacerPerson = new RacerPerson
                {
                    FirstName = name.FirstName,
                    LastName = lastNames[index].LastName,
                    Nation = lastNames[index].Nation,
                    Ability = _random.Next(10, 100),
                    Age = _random.Next(_minAge, _maxAge)
                };
                newRacerPerson.PotentialAbility = _random.Next(newRacerPerson.Ability, 100);

                if (teams[teamIndex] != null)
                {
                    newRacerPerson.Team = teams[teamIndex];
                }

                generatedRacerPeople.Add(newRacerPerson);

                teamIndex++;
                index++;

                if (teamIndex >= numberOfTeams)
                {
                    teamIndex = 0;
                }
            }

            return generatedRacerPeople;
        }

        public IList<RacerPerson> GenerateUniqueId(IList<RacerPerson> racerPeople)
        {
            foreach (var racerPerson in racerPeople)
            {
                racerPerson.RacerPersonId = Guid.NewGuid();
            }

            return racerPeople;
        }

        public IList<RacerPerson> SeasonUpdateRacerPeople(IList<RacerPerson> racerPeople)
        {
            var updateRacerPeople = new List<RacerPerson>();

            foreach (var racerPerson in racerPeople)
            {
                if (racerPerson.Age < 20)
                {
                    racerPerson.Ability += _random.Next(1, 10);
                }
                else if (racerPerson.Age < 30)
                {
                    racerPerson.Ability += _random.Next(1, 5);
                }
                else if (racerPerson.Age < 40)
                {
                    racerPerson.Ability -= _random.Next(1, 5);
                }
                else
                {
                    var list = new List<RacerPerson>();
                    var task = Task.Run(async () => { list = (List<RacerPerson>)await GenerateRacerPeople(1, 1); });
                    task.Wait();

                    var newRacerPerson = list.SingleOrDefault();
                    newRacerPerson.Team = racerPerson.Team;
                    newRacerPerson.Age = _minAge;
                    if (newRacerPerson.Ability > 50)
                    {
                        newRacerPerson.Ability = 50;
                    }
                    newRacerPerson.RacerPersonId = Guid.NewGuid();
                    updateRacerPeople.Add(newRacerPerson);
                    continue;
                }

                if (racerPerson.Ability > racerPerson.PotentialAbility)
                {
                    racerPerson.Ability = racerPerson.PotentialAbility;
                }

                if (racerPerson.Ability < 10)
                {
                    racerPerson.Ability = 10;
                }

                racerPerson.Age++;
                racerPerson.Jersey = string.Empty;
                updateRacerPeople.Add(racerPerson);
            }

            return updateRacerPeople;
        }
    }
}
