using Racing.Model;
using Racing.Repositories.RestClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.Repositories
{
    class RaceRepository : IRepository<Race>
    {
        private readonly RacingRestClient _client;

        public RaceRepository(RacingRestClient client)
        {
            _client = client;
        }

        public async Task<IList<Race>> Get()
        {
            return await _client.GetData<Race>(EndPoints.Race);
        }
    }
}
