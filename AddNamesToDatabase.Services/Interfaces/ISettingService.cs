using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddNamesToDatabase.Services.Interfaces
{
    public interface ISettingService
    {
        bool EditSetting(Setting setting);
        Task<IList<Setting>> GetSettings();
        bool AddSetting(Setting setting);
    }
}
