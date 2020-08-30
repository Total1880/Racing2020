using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Racing.Messages;
using Racing.Messages.WindowOpener;
using Racing.Model;
using Racing.Services.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Racing.ViewModel
{
    public class SeasonOverviewViewModel : ViewModelBase
    {
        private IRaceService _raceService;
        private IRacerPersonService _racerPersonService;
        private ObservableCollection<Race> _raceList;
        private ObservableCollection<string> _menu;
        private string _nextRaceName;
        private string _chosenMenuItem;
        private Division _chosenDivision;
        private int _raceLength;
        private int _seasonRaceNumber;
        private bool _nextRaceBool;
        private Visibility _endOfSeason;
        //private IList<RacerPerson> _racerPersonList;
        private IList<Division> _divisionList;
        private RelayCommand _nextRaceCommand;
        private RelayCommand _menuChoiceCommand;
        private RelayCommand _nextSeasonCommand;

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

        public Division ChosenDivision
        {
            get => _chosenDivision;
            set
            {
                if (value != null)
                {
                    _chosenDivision = value;
                    RaisePropertyChanged();
                    MessengerInstance.Send(new RaceResultPageMessage(value.DivisionId));
                    MessengerInstance.Send(new UpdateSeasonRankingMessage(value.DivisionId));
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

        public bool NextRaceBool
        {
            get => _nextRaceBool;
            set
            {
                _nextRaceBool = value;
                RaisePropertyChanged();
            }
        }

        public Visibility EndOfSeason
        {
            get => _endOfSeason;
            set
            {
                _endOfSeason = value;
                RaisePropertyChanged();
            }
        }

        //public IList<RacerPerson> RacerPersonListfdsf
        //{
        //    get => _racerPersonList;
        //    set
        //    {
        //        _racerPersonList = value;
        //        RaisePropertyChanged();
        //    }
        //}

        public IList<Division> DivisionList
        {
            get => _divisionList;
            set
            {
                _divisionList = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand NextRaceCommand => _nextRaceCommand ??= new RelayCommand(NextRace);
        public RelayCommand MenuChoiceCommand => _menuChoiceCommand ??= new RelayCommand(MenuChoice);
        public RelayCommand NextSeasonCommand => _nextSeasonCommand ??= new RelayCommand(NextSeason);

        public SeasonOverviewViewModel(IRaceService raceService, IRacerPersonService racerPersonService)
        {
            _raceService = raceService;
            _racerPersonService = racerPersonService;
            _ = GetRaces();
            MessengerInstance.Register<OverviewRacerPersonsMessage>(this, OnOpenSeasonOverviewPage);
            MessengerInstance.Register<UpdateJerseyMessage>(this, UpdateJersey);
            Menu = new ObservableCollection<string> { SeasonMenu.LatestResult, SeasonMenu.Ranking, SeasonMenu.NextRaceInfo };
            EndOfSeason = Visibility.Hidden;
            NextRaceBool = true;
        }

        private async Task GetRaces()
        {
            RaceList = new ObservableCollection<Race>(await _raceService.GetRaces());
            NextRaceName = RaceList[_seasonRaceNumber].Name;
            RaceLength = RaceList[_seasonRaceNumber].Length;
        }

        private void OnOpenSeasonOverviewPage(OverviewRacerPersonsMessage obj)
        {
            DivisionList = obj.DivisionList;
        }

        private void UpdateJersey(UpdateJerseyMessage obj)
        {
            foreach (var team in DivisionList.Where(d => d.DivisionId == obj.DivisionId).FirstOrDefault().TeamList)
            {
                foreach (var racerPeople in team.RacerPeople)
                {
                    if (racerPeople.RacerPersonId != obj.YellowId)
                    {
                        racerPeople.Jersey = string.Empty;
                    }
                }
                if (team.RacerPeople.Any(r => r.RacerPersonId == obj.YellowId))
                {
                    team.RacerPeople.Where(r => r.RacerPersonId == obj.YellowId).FirstOrDefault().Jersey = "Yellow";
                }
            }

        }

        private void NextRace()
        {
            MessengerInstance.Send(new OpenRaceResultPageMessage());

            foreach (var division in DivisionList)
            {
                MessengerInstance.Send(new RaceResultPageMessage(division.TeamList, RaceList[_seasonRaceNumber], division.DivisionId));
            }

            if (_seasonRaceNumber + 1 < RaceList.Count)
            {
                _seasonRaceNumber++;
            }
            else
            {
                _seasonRaceNumber = 0;
                EndOfSeason = Visibility.Visible;
                NextRaceBool = false;
            }

            _ = GetRaces();

            ChosenMenuItem = SeasonMenu.LatestResult;

            if (ChosenDivision != null)
            {
                MessengerInstance.Send(new RaceResultPageMessage(ChosenDivision.DivisionId));
                MessengerInstance.Send(new UpdateSeasonRankingMessage(ChosenDivision.DivisionId));
            }
        }

        private void NextSeason()
        {
            MessengerInstance.Send(new ResetSeasonMessage(DivisionList));
            EndOfSeason = Visibility.Hidden;
            NextRaceBool = true;

            foreach (var division in DivisionList)
            {
                foreach (var team in division.TeamList)
                {
                    team.RacerPeople = _racerPersonService.SeasonUpdateRacerPeople(team.RacerPeople);
                }
            }
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
                case SeasonMenu.NextRaceInfo:
                    MessengerInstance.Send(new OpenNextRaceInfoPageMessage());
                    MessengerInstance.Send(new NextRaceInfoMessage(RaceList[_seasonRaceNumber]));
                    break;
                default:
                    break;
            }
        }
    }
}
