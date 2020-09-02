using Racing.Model;
using Racing.Repositories;
using Racing.Services.Interfaces;
using System.Collections.Generic;

namespace Racing.Services
{
    public class SaveGameDivisionService : ISaveGameDivisionService
    {
        private readonly ISaveGameRepository<Division> _saveGameDivisionRepository;

        public SaveGameDivisionService(ISaveGameRepository<Division> saveGameDivisionRepository)
        {
            _saveGameDivisionRepository = saveGameDivisionRepository;
        }
        public IList<Division> GetDivisions()
        {
            return _saveGameDivisionRepository.Load();
        }

        public bool SaveDivisions(IList<Division> divisions)
        {
            return _saveGameDivisionRepository.Save(divisions);
        }
    }
}
