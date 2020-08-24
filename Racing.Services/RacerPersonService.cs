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
                RacerPerson newRacerPerson = new RacerPerson { FirstName = name.FirstName };
                newRacerPerson.LastName = lastNames[index].LastName;
                newRacerPerson.Nation = lastNames[index].Nation;
                newRacerPerson.Ability = _random.Next(1, 20);
                newRacerPerson.RacerPersonId = index;
                newRacerPerson.Team = teams[teamIndex];

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
    }
}
