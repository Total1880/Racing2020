using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing.Messages;
using Racing.Model;
using Racing.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Racing.ViewModel
{
    public class RacePageViewModel : ViewModelBase
    {
        private readonly IRaceEngineService _raceEngineService;
        private IList<RacerPerson> _racerPeople;
        private ObservableCollection<Racer> _racerList;
        private Division _division;
        private Race _race;
        private RelayCommand _nextStepCommand;

        public ObservableCollection<Racer> RacerList
        {
            get => _racerList;
            set
            {
                _racerList = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand NextStepCommand => _nextStepCommand ??= new RelayCommand(NextStep);

        public RacePageViewModel(IRaceEngineService raceEngineService)
        {
            _raceEngineService = raceEngineService;
            MessengerInstance.Register<RaceSetupMessage>(this, Initialize);
        }

        private void Initialize(RaceSetupMessage obj)
        {
            _division = obj.Division;
            _race = obj.Race;
            _racerPeople = new List<RacerPerson>();

            foreach (var team in _division.TeamList)
            {
                foreach (var racerPerson in team.RacerPeople)
                {
                    _racerPeople.Add(racerPerson);
                }
            }

            _raceEngineService.Go(_racerPeople, _race);
            RacerList = new ObservableCollection<Racer>(_raceEngineService.GetRacerList());
        }

        private void NextStep()
        {
            _raceEngineService.Move();
            RacerList = new ObservableCollection<Racer>(_raceEngineService.GetRacerList().OrderByDescending(r => r.RacePosition));
        }
    }
}
