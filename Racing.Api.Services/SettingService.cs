using Racing.Api.Repositories;
using Racing.Api.Services.Interfaces;
using Racing.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Racing.Api.Services
{
    public class SettingService : ISettingService
    {
        private readonly IRepository<Setting> _settingRepository;

        public SettingService(IRepository<Setting> settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public bool EditSetting(Setting setting)
        {
            return _settingRepository.Update(setting);
        }

        public Setting GetSettingByDescription(string description)
        {
            return GetSettings().Where(s => s.Description == description).FirstOrDefault();
        }

        public IList<Setting> GetSettings()
        {
            return _settingRepository.Get();
        }
    }
}
