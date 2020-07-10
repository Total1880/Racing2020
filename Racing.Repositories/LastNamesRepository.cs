using Racing.Model;
using Racing.Repositories.RestClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.Repositories
{
    public class LastNamesRepository : INamesRepository<LastNames>
    {
        private readonly RacingRestClient _client;

        public LastNamesRepository(RacingRestClient client)
        {
            _client = client;
        }

        public async Task<IList<LastNames>> GenerateNames(int numberOfPeople)
        {
            return await _client.GenerateNames<LastNames>(EndPoints.LastName, numberOfPeople);
        }
    }
}
