using Racing.Api.Repositories;
using Racing.Api.Services.Interfaces;
using Racing.Model;
using System.Collections.Generic;

namespace Racing.Api.Services
{
    public class FirstNamesService : IFirstNamesService
    {
        private readonly INamesRepository<FirstNames> _firstNamesRepository;

        public FirstNamesService(INamesRepository<FirstNames> firstNamesRepository)
        {
            _firstNamesRepository = firstNamesRepository;
        }

        public bool CreateNames(IList<FirstNames> firstNames)
        {
            return _firstNamesRepository.CreateNames(firstNames);
        }

        public IList<FirstNames> GenerateFirstNames(int numberOfNames)
        {
            return _firstNamesRepository.GenerateNames(numberOfNames);
        }
    }
}
