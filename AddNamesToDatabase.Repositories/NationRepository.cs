using AddNamesToDatabase.Repositories.RestClient;
using Racing.Api.Repositories;
using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddNamesToDatabase.Repositories
{
    public class NationRepository : IRepository<Nation>
    {
        private readonly RacingRestClient _client;

        public NationRepository(RacingRestClient racingRestClient)
        {
            _client = racingRestClient;
        }

        public bool Create(Nation item)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<Nation>> Get()
        {
            return await _client.GetData<Nation>(Endpoints.Nation);
        }

        public bool Update(Nation item)
        {
            throw new System.NotImplementedException();
        }
    }
}
