using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing.Messages;
using Racing.Model;
using Racing.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Racing.ViewModel
{
    public class SeasonOverviewViewModel : ViewModelBase
    {
        private IRaceService _raceService;
        private IRaceEngineService _raceEngineService;
        private ObservableCollection<Race> _raceList;
        private string _nextRaceName;
        private int _raceLength;
        private int _seasonRaceNumber;
        private IList<RacerPerson> _racerPersonList;
        private RelayCommand _nextRaceCommand;

        public ObservableCollection<Race> RaceList
        {
            get => _raceList;
            set
            {
                _raceList = value;
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

        public SeasonOverviewViewModel(IRaceService raceService, IRaceEngineService raceEngineService)
        {
            _raceService = raceService;
            _raceEngineService = raceEngineService;
            _ = GetRaces();
            MessengerInstance.Register<OverviewRacerPersonsMessage>(this, OnOpenSeasonOverviewPage);

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
            _raceEngineService.Go(RacerPersonList, RaceLength);
            RacerPersonList.Clear();
            RacerPersonList = _raceEngineService.GetFinishRanking();

            if (_seasonRaceNumber + 1 < RaceList.Count)
            {
                _seasonRaceNumber++;
            }
            else
            {
                _seasonRaceNumber = 0;
            }
            _ = GetRaces();
        }
    }
}
