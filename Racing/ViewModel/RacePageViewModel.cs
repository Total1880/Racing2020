using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing.Messages;
using Racing.Messages.WindowOpener;
using Racing.Model;
using Racing.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Racing.ViewModel
{
    public class RacePageViewModel : ViewModelBase
    {
        private readonly IRaceEngineService _raceEngineService;
        private IList<RacerPerson> _racerPeople;
        private IList<RacerPerson> _finishRanking;
        private ObservableCollection<Racer> _racerList;
        private Division _division;
        private Race _race;
        private RelayCommand _nextStepCommand;
        private RelayCommand _fullRaceCommand;
        private RelayCommand _runRaceCommand;
        private RelayCommand _pauseRaceCommand;
        private RelayCommand _finishRaceCommand;
        private bool _raceHasFinished;
        private bool _racePaused;
        private bool _goButtonEnabled;
        private bool _oneStepButtonEnabled;
        private bool _pauseButtonEnabled;
        private bool _fullRaceButtonEnabled;

        public IList<RacerPerson> FinishRanking
        {
            get => _finishRanking;
            set
            {
                _finishRanking = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Racer> RacerList
        {
            get => _racerList;
            set
            {
                _racerList = value;
                RaisePropertyChanged();
            }
        }

        public bool RaceHasFinished
        {
            get => _raceHasFinished;
            set
            {
                _raceHasFinished = value;

                if (value == true)
                {
                    GoButtonEnabled = false;
                    OneStepButtonEnabled = false;
                    PauseButtonEnabled = false;
                    FullRaceButtonEnabled = false;
                }

                RaisePropertyChanged();
            }
        }

        public bool GoButtonEnabled
        {
            get => _goButtonEnabled;
            set
            {
                _goButtonEnabled = value;
                RaisePropertyChanged();
            }
        }
        public bool OneStepButtonEnabled
        {
            get => _oneStepButtonEnabled;
            set
            {
                _oneStepButtonEnabled = value;
                RaisePropertyChanged();
            }
        }
        public bool PauseButtonEnabled
        {
            get => _pauseButtonEnabled;
            set
            {
                _pauseButtonEnabled = value;
                RaisePropertyChanged();
            }
        }
        public bool FullRaceButtonEnabled
        {
            get => _fullRaceButtonEnabled;
            set
            {
                _fullRaceButtonEnabled = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand NextStepCommand => _nextStepCommand ??= new RelayCommand(NextStep);
        public RelayCommand FullRaceCommand => _fullRaceCommand ??= new RelayCommand(FullRace);
        public RelayCommand RunRaceCommand => _runRaceCommand ??= new RelayCommand(RunRace);
        public RelayCommand PauseRaceCommand => _pauseRaceCommand ??= new RelayCommand(PauseRace);
        public RelayCommand FinishRaceCommand => _finishRaceCommand ??= new RelayCommand(FinishRace);

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
            _racePaused = true;
            GoButtonEnabled = true;
            OneStepButtonEnabled = true;
            PauseButtonEnabled = false;
            FullRaceButtonEnabled = true;


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
            RaceHasFinished = _raceEngineService.RaceHasEnded();
            FinishRanking = _raceEngineService.GetFinishRanking();
        }

        private void FullRace()
        {
            do
            {
                _raceEngineService.Move();
                RacerList = new ObservableCollection<Racer>(_raceEngineService.GetRacerList().OrderByDescending(r => r.RacePosition));
                FinishRanking = _raceEngineService.GetFinishRanking();
                RaceHasFinished = _raceEngineService.RaceHasEnded();
            } while (RaceHasFinished == false);
        }

        private void RunRace()
        {
            _racePaused = false;
            GoButtonEnabled = false;
            PauseButtonEnabled = true;
            _ = RunRaceAsync();
        }

        private void PauseRace()
        {
            _racePaused = true;
            GoButtonEnabled = true;
            PauseButtonEnabled = false;
        }

        private async Task RunRaceAsync()
        {
            while (_racePaused == false && RaceHasFinished == false)
            {
                await Task.Delay(1000);
                _raceEngineService.Move();
                RacerList = new ObservableCollection<Racer>(_raceEngineService.GetRacerList().OrderByDescending(r => r.RacePosition));
                FinishRanking = _raceEngineService.GetFinishRanking();
                RaceHasFinished = _raceEngineService.RaceHasEnded();
            }
        }

        private void FinishRace()
        {
            MessengerInstance.Send(new OpenSeasonOverviewPageMessage());
            FinishRanking.Clear();
        }
    }
}
