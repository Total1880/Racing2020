using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Racing.Messages;
using Racing.Messages.WindowOpener;
using Racing.Model;
using Racing.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Racing.ViewModel
{
    public class SeasonOverviewViewModel : ViewModelBase
    {
        private readonly IRaceService _raceService;
        private readonly IRacerPersonService _racerPersonService;
        private readonly ISaveGameDivisionService _saveGameDivisionService;
        private readonly ISeasonEngineService _seasonEngineService;
        private readonly IFacilityUpgradeEngine _facilityUpgradeEngine;
        private ObservableCollection<Race> _raceList;
        private ObservableCollection<string> _menu;
        private string _nextRaceName;
        private string _chosenMenuItem;
        private Division _chosenDivision;
        private int _raceLength;
        private int _seasonRaceNumber;
        private bool _nextRaceBool;
        private Visibility _endOfSeason;
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
                    MessengerInstance.Send(new RaceResultPageMessage(value));
                    MessengerInstance.Send(new UpdateSeasonAfterRaceMessage(value));
                    
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

        public SeasonOverviewViewModel(
            IRaceService raceService, 
            IRacerPersonService racerPersonService, 
            ISaveGameDivisionService saveGameDivisionService, 
            ISeasonEngineService seasonEngineService,
            IFacilityUpgradeEngine facilityUpgradeEngine)
        {
            _raceService = raceService;
            _racerPersonService = racerPersonService;
            _saveGameDivisionService = saveGameDivisionService;
            _seasonEngineService = seasonEngineService;
            _facilityUpgradeEngine = facilityUpgradeEngine;
            _ = GetRaces();
            MessengerInstance.Register<OverviewRacerPersonsMessage>(this, OnOpenSeasonOverviewPage);
            MessengerInstance.Register<UpdateJerseyMessage>(this, UpdateJersey);
            MessengerInstance.Register<UpdateSeasonAfterRaceMessage>(this, UpdateFinance);
            Menu = new ObservableCollection<string> { SeasonMenu.LatestResult, SeasonMenu.Ranking, SeasonMenu.NextRaceInfo, SeasonMenu.TeamOverview };
            EndOfSeason = Visibility.Hidden;
            NextRaceBool = true;
        }

        private void UpdateFinance(UpdateSeasonAfterRaceMessage obj)
        {
            if (obj.Race == null)
            {
                return;
            }

            var teams = DivisionList.Where(d => d.DivisionId == obj.Division.DivisionId).FirstOrDefault().TeamList;

            teams = _seasonEngineService.UpdateFinances(teams, obj.RacerPersonList, obj.Race, obj.Division.Tier);
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
            _saveGameDivisionService.SaveDivisions(DivisionList);
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
            foreach (var division in DivisionList)
            {
                MessengerInstance.Send(new OpenRacePageMessage());
                MessengerInstance.Send(new RaceSetupMessage(division, RaceList[_seasonRaceNumber]));
            }
            
            //MessengerInstance.Send(new OpenRaceResultPageMessage());

            //foreach (var division in DivisionList)
            //{
            //    MessengerInstance.Send(new RaceResultPageMessage(division.TeamList, RaceList[_seasonRaceNumber], division));
            //}

            //if (_seasonRaceNumber + 1 < RaceList.Count)
            //{
            //    _seasonRaceNumber++;
            //}
            //else
            //{
            //    _seasonRaceNumber = 0;
            //    EndOfSeason = Visibility.Visible;
            //    NextRaceBool = false;
            //}

            //_ = GetRaces();

            //ChosenMenuItem = SeasonMenu.LatestResult;

            //if (ChosenDivision != null)
            //{
            //    MessengerInstance.Send(new RaceResultPageMessage(ChosenDivision));
            //    MessengerInstance.Send(new UpdateSeasonAfterRaceMessage(ChosenDivision));
            //}
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

            DivisionList = _facilityUpgradeEngine.Upgrade(DivisionList);
            
            _saveGameDivisionService.SaveDivisions(DivisionList);
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
                case SeasonMenu.TeamOverview:
                    MessengerInstance.Send(new OpenTeamOverviewMessage());
                    if (ChosenDivision != null)
                    {
                        MessengerInstance.Send(new UpdateSeasonAfterRaceMessage(ChosenDivision));
                    }
                    else
                    {
                        MessengerInstance.Send(new UpdateSeasonAfterRaceMessage(DivisionList[0]));
                    }

                    break;
                default:
                    break;
            }
        }
    }
}
