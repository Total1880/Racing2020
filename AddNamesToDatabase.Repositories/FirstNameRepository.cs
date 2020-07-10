using AddNamesToDatabase.Repositories.RestClient;
using Racing.Model;
using System.Collections.Generic;

namespace AddNamesToDatabase.Repositories
{
    public class FirstNameRepository : INameRepository<FirstNames>
    {
        private readonly RacingRestClient _client;

        public FirstNameRepository(RacingRestClient client)
        {
            _client = client;
        }

        public bool CreateNames(IList<FirstNames> names)
        {
            return _client.CreateData(Endpoints.FirstName, names);
        }
    }
}
