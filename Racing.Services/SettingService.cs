using Racing.Model;
using Racing.Repositories;
using Racing.Services.Interfaces;
using System.Threading.Tasks;

namespace Racing.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;

        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }
        public async Task<Setting> GetSettingByDescription(string description)
        {
            return await _settingRepository.GetSettingByDescription(description);
        }
    }
}
