using AddNamesToDatabase.Repositories.RestClient;
using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddNamesToDatabase.Repositories
{
    public class RaceRepository : IRepository<Race>
    {
        private readonly RacingRestClient _client;

        public RaceRepository(RacingRestClient client)
        {
            _client = client;
        }

        public bool Create(Race item)
        {
            return _client.CreateData(Endpoints.Race, item);
        }

        public bool Delete(int id)
        {
            return _client.DeleteData(Endpoints.Race, id);
        }

        public async Task<IList<Race>> Get()
        {
            return await _client.GetData<Race>(Endpoints.Race);
        }

        public bool Update(Race item)
        {
            return _client.UpdateData(Endpoints.Race, item);
        }
    }
}
