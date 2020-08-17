using AddNamesToDatabase.Services.Interfaces;
using GalaSoft.MvvmLight;

namespace AddNamesToDatabase.ViewModel
{
    public class RacePageViewModel : ViewModelBase
    {
        private readonly IRaceService _raceService;

        public RacePageViewModel(IRaceService raceService)
        {
            _raceService = raceService;
        }
    }
}
