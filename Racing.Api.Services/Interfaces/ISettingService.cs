using Racing.Model;
using System.Collections.Generic;

namespace Racing.Api.Services.Interfaces
{
    public interface ISettingService
    {
        bool EditSetting(Setting setting);
        IList<Setting> GetSettings();
        Setting GetSettingByDescription(string description);
    }
}
