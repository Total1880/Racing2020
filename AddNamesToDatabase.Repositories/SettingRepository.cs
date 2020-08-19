using AddNamesToDatabase.Repositories.RestClient;
using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddNamesToDatabase.Repositories
{
    public class SettingRepository : IRepository<Setting>
    {
        private readonly RacingRestClient _client;

        public SettingRepository(RacingRestClient client)
        {
            _client = client;
        }
        public bool Create(Setting item)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<Setting>> Get()
        {
            return await _client.GetData<Setting>(Endpoints.Setting);
        }

        public bool Update(Setting item)
        {
            return _client.UpdateData(Endpoints.Setting, item);
        }
    }
}
