using AddNamesToDatabase.Repositories.RestClient;
using Racing.Model;
using System.Collections.Generic;

namespace AddNamesToDatabase.Repositories
{
    class LastNameRepository : INameRepository<LastNames>
    {
        private readonly RacingRestClient _client;

        public LastNameRepository(RacingRestClient client)
        {
            _client = client;
        }

        public bool CreateNames(IList<LastNames> names)
        {
            return _client.CreateData(Endpoints.LastName, names);
        }
    }
}
