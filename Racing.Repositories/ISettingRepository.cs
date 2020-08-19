using Racing.Model;
using System.Threading.Tasks;

namespace Racing.Repositories
{
    public interface ISettingRepository
    {
        Task<Setting> GetSettingByDescription(string description);
    }
}
