using AddNamesToDatabase.Repositories.RestClient;
using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddNamesToDatabase.Repositories
{
    public class TeamRepository : IRepository<Team>
    {
        private readonly RacingRestClient _client;

        public TeamRepository(RacingRestClient client)
        {
            _client = client;
        }
        public bool Create(Team item)
        {
            return _client.CreateData(Endpoints.Team, item);
        }

        public bool Delete(int id)
        {
            return _client.DeleteData(Endpoints.Team, id);
        }

        public async Task<IList<Team>> Get()
        {
            return await _client.GetData<Team>(Endpoints.Team);
        }

        public bool Update(Team item)
        {
            return _client.UpdateData(Endpoints.Team, item);
        }
    }
}
