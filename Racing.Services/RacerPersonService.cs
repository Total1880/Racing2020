using Racing.Repositories;
using Racing.Model;
using Racing.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Racing.Services
{
    public class RacerPersonService : IRacerPersonService
    {
        private INamesRepository<FirstNames> _firstNamesRepository;
        private INamesRepository<LastNames> _lastNamesRepository;
        private IRepository<Team> _teamRepository;
        private readonly Random _random = new Random();
        private int _minAge = 18;
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
            int index = 0;
            int teamIndex = 0;

            foreach (var name in firstNames)
            {
                RacerPerson newRacerPerson = new RacerPerson
                {
                    FirstName = name.FirstName,
                    LastName = lastNames[index].LastName,
                    Nation = lastNames[index].Nation,
                    Ability = _random.Next(1, 100),
                    Age = _random.Next(_minAge, _maxAge),
                    RacerPersonId = index
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

        public IList<RacerPerson> SeasonUpdateRacerPeople(IList<RacerPerson> racerPeople)
        {
            var updateRacerPeople = new List<RacerPerson>();

            foreach (var racerPerson in racerPeople)
            {
                if (racerPerson.Age < 20)
                {
                    racerPerson.Ability += _random.Next(1, 10);
                }
                else if(racerPerson.Age < 30)
                {
                    racerPerson.Ability += _random.Next(1, 5);
                }
                else if (racerPerson.Age < 40)
                {
                    racerPerson.Ability -= _random.Next(1, 5);
                }
                else
                {
                    racerPerson.Ability -= _random.Next(1, 10);
                }

                if (racerPerson.Ability > racerPerson.PotentialAbility)
                {
                    racerPerson.Ability = racerPerson.PotentialAbility;
                }

                if (racerPerson.Ability <= 0)
                {
                    racerPerson.Ability = 1;
                }

                racerPerson.Age++;
                updateRacerPeople.Add(racerPerson);
            }

            return updateRacerPeople;
        }
    }
}
