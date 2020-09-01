using AddNamesToDatabase.Services.Interfaces;
using AddNamesToDatabase.Repositories;
using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddNamesToDatabase.Services
{
    public class SettingService : ISettingService
    {
        private readonly IRepository<Setting> _settingRepository;

        public SettingService(IRepository<Setting> settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public bool AddSetting(Setting setting)
        {
            return _settingRepository.Create(setting);
        }

        public bool EditSetting(Setting setting)
        {
            return _settingRepository.Update(setting);
        }

        public async Task<IList<Setting>> GetSettings()
        {
            return await _settingRepository.Get();
        }
    }
}
