using GalaSoft.MvvmLight;
using Racing.Model;
using Racing.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.ViewModel
{
    public class HomePageViewModel : ViewModelBase
    {
        private IRacerPersonService _racerPersonService;
        private IList<RacerPerson> _racerList;
        private int _numberOfNewRacers = 20;

        public IList<RacerPerson> RacerList
        {
            get => _racerList;
            set
            {
                _racerList = value;
                RaisePropertyChanged();
            }
        }

        public HomePageViewModel(IRacerPersonService racerPersonService)
        {
            _racerPersonService = racerPersonService;
            _ = GenerateNewGame();
        }

        private async Task GenerateNewGame()
        {
            RacerList = await _racerPersonService.GenerateRacerPeople(_numberOfNewRacers);
        }
    }
}
