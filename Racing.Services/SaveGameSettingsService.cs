using Racing.Model;
using Racing.Repositories;
using Racing.Services.Interfaces;
using System;

namespace Racing.Services
{
    public class SaveGameSettingsService : ISaveGameSettingsService
    {
        private readonly ISaveGameRepository<Division> _saveGameRepository;

        public SaveGameSettingsService(ISaveGameRepository<Division> saveGameRepository)
        {
            _saveGameRepository = saveGameRepository;
        }
        public int GetPlayerTeamId()
        {
            return _saveGameRepository.GetPlayerTeamId();
        }

        public bool SaveGameSettings(int playerTeamId)
        {
            return _saveGameRepository.SaveGameSettings(playerTeamId);
        }
    }
}
