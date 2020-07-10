using Racing.Model;
using Racing.Repositories.RestClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.Repositories
{
    public class FirstNamesRepository : INamesRepository<FirstNames>
    {
        private readonly RacingRestClient _client;

        public FirstNamesRepository(RacingRestClient client)
        {
            _client = client;
        }

        public async Task<IList<FirstNames>> GenerateNames(int numberOfPeople)
        {
            return await _client.GenerateNames<FirstNames>(EndPoints.FirstName, numberOfPeople);
        }
    }
}
