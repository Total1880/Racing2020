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
        private readonly Random _random = new Random();

        public RacerPersonService(INamesRepository<FirstNames> firstNamesRepository, INamesRepository<LastNames> lastNamesRepository)
        {
            _firstNamesRepository = firstNamesRepository;
            _lastNamesRepository = lastNamesRepository;
        }

        public async Task<IList<RacerPerson>> GenerateRacerPeople(int numberOfPeople)
        {
            var firstNames = await _firstNamesRepository.GenerateNames(numberOfPeople);
            var lastNames = await _lastNamesRepository.GenerateNames(numberOfPeople);
            var generatedRacerPeople = new List<RacerPerson>();
            int index = 0;

            foreach (var name in firstNames)
            {
                RacerPerson newRacerPerson = new RacerPerson { FirstName = name.FirstName };
                newRacerPerson.LastName = lastNames[index].LastName;
                newRacerPerson.Nation = lastNames[index].Nation;
                newRacerPerson.Ability = _random.Next(1, 20);

                generatedRacerPeople.Add(newRacerPerson);

                index++;
            }

            return generatedRacerPeople;
        }
    }
}
