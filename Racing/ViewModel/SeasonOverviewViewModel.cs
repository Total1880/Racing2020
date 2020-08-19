using GalaSoft.MvvmLight;
using Racing.Model;
using Racing.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Racing.ViewModel
{
    public class SeasonOverviewViewModel : ViewModelBase
    {
        private IRaceService _raceService;
        private ObservableCollection<Race> _raceList;

        public ObservableCollection<Race> RaceList
        {
            get => _raceList;
            set
            {
                _raceList = value;
                RaisePropertyChanged();
            }
        }

        public SeasonOverviewViewModel(IRaceService raceService)
        {
            _raceService = raceService;
            _ = GetRaces();
        }

        private async Task GetRaces()
        {
            RaceList = new ObservableCollection<Race>(await _raceService.GetRaces());
        }
    }
}
