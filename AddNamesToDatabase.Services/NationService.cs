using AddNamesToDatabase.Services.Interfaces;
using Racing.Api.Repositories;
using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddNamesToDatabase.Services
{
    public class NationService : INationService
    {
        private readonly IRepository<Nation> _nationRepository;

        public NationService(IRepository<Nation> nationRepository)
        {
            _nationRepository = nationRepository;
        }

        public bool CreateNation(Nation nation)
        {
            return _nationRepository.Create(nation);
        }

        public Task<IList<Nation>> GetNations()
        {
            return _nationRepository.Get();
        }
    }
}
