using Racing.Api.Repositories;
using Racing.Api.Services.Interfaces;
using Racing.Model;
using System;
using System.Collections.Generic;

namespace Racing.Api.Services
{
    public class LastNamesService : ILastNamesService
    {
        private readonly INamesRepository<LastNames> _lastNamesRepository;

        public LastNamesService(INamesRepository<LastNames> lastNamesRepository)
        {
            _lastNamesRepository = lastNamesRepository;
        }

        public bool CreateNames(IList<LastNames> lastNames)
        {
            return _lastNamesRepository.CreateNames(lastNames);
        }
    }
}
