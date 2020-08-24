using Racing.Model;
using Racing.Repositories.RestClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.Repositories
{
    public class TeamRepository : IRepository<Team>
    {
        private readonly RacingRestClient _client;

        public TeamRepository(RacingRestClient client)
        {
            _client = client;
        }
        public async Task<IList<Team>> Get()
        {
            return await _client.GetData<Team>(EndPoints.Team);
        }
    }
}
