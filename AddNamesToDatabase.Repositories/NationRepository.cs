using AddNamesToDatabase.Repositories.RestClient;
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
            return _client.CreateData(Endpoints.Nation, item);
        }

        public bool Delete(int id)
        {
            return _client.DeleteData(Endpoints.Nation, id);
        }

        public async Task<IList<Nation>> Get()
        {
            return await _client.GetData<Nation>(Endpoints.Nation);
        }

        public bool Update(Nation item)
        {
            return _client.UpdateData(Endpoints.Nation, item);
        }
    }
}
