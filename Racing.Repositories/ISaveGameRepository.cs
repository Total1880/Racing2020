using System.Collections;
using System.Collections.Generic;

namespace Racing.Repositories
{
    public interface ISaveGameRepository<T>
    {
        bool Save(IList<T> items);
        IList<T> Load();
        bool SaveGameSettings(int playerTeamId);
        int GetPlayerTeamId();
    }
}
