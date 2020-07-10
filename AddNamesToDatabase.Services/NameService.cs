using AddNamesToDatabase.Repositories;
using AddNamesToDatabase.Services.Interfaces;
using Racing.Model;
using System.Collections.Generic;

namespace AddNamesToDatabase.Services
{
    public class NameService : INameService
    {
        private readonly INameRepository<FirstNames> _firstNameRepository;
        private readonly INameRepository<LastNames> _lastNameRepository;

        public NameService(INameRepository<FirstNames> firstNameRepository, INameRepository<LastNames> lastNameRepository)
        {
            _firstNameRepository = firstNameRepository;
            _lastNameRepository = lastNameRepository;
        }

        public bool CreateNames(IList<FirstNames> firstNames, IList<LastNames> lastNames)
        {
            return _firstNameRepository.CreateNames(firstNames) && _lastNameRepository.CreateNames(lastNames);
        }
    }
}
