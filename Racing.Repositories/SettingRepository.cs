using Racing.Model;
using Racing.Repositories.RestClient;
using System.Threading.Tasks;

namespace Racing.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        private readonly RacingRestClient _client;

        public SettingRepository(RacingRestClient client)
        {
            _client = client;
        }

        public async Task<Setting> GetSettingByDescription(string description)
        {
            return await _client.GetDataByString<Setting>(EndPoints.Setting, description);
        }
    }
}
