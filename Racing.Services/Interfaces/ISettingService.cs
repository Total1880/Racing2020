using Racing.Model;
using System.Threading.Tasks;

namespace Racing.Services.Interfaces
{
    public interface ISettingService
    {
        Task<Setting> GetSettingByDescription(string description);
    }
}
