using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing.Messages;
using Racing.Messages.WindowOpener;
using Racing.Model;
using Racing.Services.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Racing.ViewModel
{
    public class SeasonOverviewViewModel : ViewModelBase
    {
        private IRaceService _raceService;
        private ObservableCollection<Race> _raceList;
        private ObservableCollection<string> _menu;
        private string _nextRaceName;
        private string _chosenMenuItem;
        private int _raceLength;
        private int _seasonRaceNumber;
        private IList<RacerPerson> _racerPersonList;
        private RelayCommand _nextRaceCommand;
        private RelayCommand _menuChoiceCommand;

        public ObservableCollection<Race> RaceList
        {
            get => _raceList;
            set
            {
                _raceList = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> Menu
        {
            get => _menu;
            set
            {
                _menu = value;
                RaisePropertyChanged();
            }
        }

        public string NextRaceName 
        { 
            get => _nextRaceName;
            set
            {
                _nextRaceName = value;
                RaisePropertyChanged();
            } 
        }

        public string ChosenMenuItem
        {
            get => _chosenMenuItem;
            set
            {
                if (value != null)
                {
                    _chosenMenuItem = value;
                    MenuChoice();
                    RaisePropertyChanged();
                }
            }
        }

        public int RaceLength
        {
            get => _raceLength;
            set
            {
                _raceLength = value;
                RaisePropertyChanged();
            }
        }

        public IList<RacerPerson> RacerPersonList
        {
            get => _racerPersonList;
            set
            {
                _racerPersonList = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand NextRaceCommand => _nextRaceCommand ??= new RelayCommand(NextRace);
        public RelayCommand MenuChoiceCommand => _menuChoiceCommand ??= new RelayCommand(MenuChoice);

        public SeasonOverviewViewModel(IRaceService raceService)
        {
            _raceService = raceService;
            _ = GetRaces();
            MessengerInstance.Register<OverviewRacerPersonsMessage>(this, OnOpenSeasonOverviewPage);
            Menu = new ObservableCollection<string>{SeasonMenu.LatestResult, SeasonMenu.Ranking};
        }

        private async Task GetRaces()
        {
            RaceList = new ObservableCollection<Race>(await _raceService.GetRaces());
            NextRaceName = RaceList[_seasonRaceNumber].Name;
            RaceLength = RaceList[_seasonRaceNumber].Length;
        }

        private void OnOpenSeasonOverviewPage(OverviewRacerPersonsMessage obj)
        {
            RacerPersonList = obj.RacerList;
        }

        private void NextRace()
        {
            MessengerInstance.Send(new OpenRaceResultPageMessage());
            MessengerInstance.Send(new RaceResultPageMessage(RacerPersonList, RaceList[_seasonRaceNumber]));

            if (_seasonRaceNumber + 1 < RaceList.Count)
            {
                _seasonRaceNumber++;
            }
            else
            {
                _seasonRaceNumber = 0;
            }
            _ = GetRaces();

            ChosenMenuItem = SeasonMenu.LatestResult;
        }

        public void MenuChoice()
        {
            switch (ChosenMenuItem)
            {
                case SeasonMenu.LatestResult:
                    MessengerInstance.Send(new OpenRaceResultPageMessage());
                    break;
                case SeasonMenu.Ranking:
                    MessengerInstance.Send(new OpenSeasonRankingPageMessage());
                    break;
                default:
                    break;
            }
        }
    }
}
