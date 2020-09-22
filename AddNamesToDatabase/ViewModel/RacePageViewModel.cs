using AddNamesToDatabase.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing.Model;
using Racing.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AddNamesToDatabase.ViewModel
{
    public class RacePageViewModel : ViewModelBase
    {
        private readonly IRaceService _raceService;
        private readonly ISettingService _settingService;
        private string _raceName;
        private int _raceLength;
        private int _prizeMoneyForOnePoint;
        private ObservableCollection<Race> _raceList;
        private Race _selectedRace;
        private RelayCommand _addRaceCommand;
        private RelayCommand _editRaceCommand;
        private RelayCommand _deleteRaceCommand;
        private RelayCommand _newRacePointListCommand;
        private IList<RacePoint> _racePointList;
        private RacePoint _selectedRacePoint;

        public string RaceName
        {
            get => _raceName;
            set
            {
                _raceName = value;
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

        public int PrizeMoneyForOnePoint
        {
            get => _prizeMoneyForOnePoint;
            set
            {
                _prizeMoneyForOnePoint = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Race> RaceList
        {
            get => _raceList;
            set
            {
                _raceList = value;
                RaisePropertyChanged();
            }
        }

        public Race SelectedRace
        {
            get => _selectedRace;
            set
            {
                if (value != null)
                {
                    _selectedRace = value;
                    RaceName = value.Name;
                    RaceLength = value.Length;
                    RacePointList = value.RacePointList;
                    PrizeMoneyForOnePoint = value.PrizeMoneyForOnePoint;
                }
            }
        }

        public IList<RacePoint> RacePointList
        {
            get => _racePointList.OrderBy(p => p.Position).ToList();
            set
            {
                _racePointList = value;
                RaisePropertyChanged();
            }
        }

        public RacePoint SelectedRacePoint
        {
            get => _selectedRacePoint;
            set
            {
                _selectedRacePoint = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand AddRaceCommand => _addRaceCommand ??= new RelayCommand(AddRace);
        public RelayCommand EditRaceCommand => _editRaceCommand ??= new RelayCommand(EditRace);
        public RelayCommand DeleteRaceCommand => _deleteRaceCommand ??= new RelayCommand(DeleteRace);
        public RelayCommand NewRacePointListCommand => _newRacePointListCommand ??= new RelayCommand(NewRacePointListSync);

        public RacePageViewModel(IRaceService raceService, ISettingService settingService)
        {
            _raceService = raceService;
            _settingService = settingService;
            _ = GetRaces();
            _ = NewRacePointList();
            RacePointList = new List<RacePoint>();
        }

        public async Task GetRaces()
        {
            RaceList = new ObservableCollection<Race>(await _raceService.GetRaces());
        }

        private void AddRace()
        {
            var newRace = new Race { Name = RaceName, Length = RaceLength, RacePointList = RacePointList, PrizeMoneyForOnePoint = PrizeMoneyForOnePoint };

            if (newRace.Length > 10 && newRace.Length <= 1000)
            {
                if (_raceService.CreateRace(newRace))
                {
                    ResetPage();
                }
            }

            _ = GetRaces();
        }

        private void EditRace()
        {
            if (SelectedRace != null)
            {
                SelectedRace.Name = RaceName;
                SelectedRace.Length = RaceLength;
                SelectedRace.RacePointList = RacePointList;
                SelectedRace.PrizeMoneyForOnePoint = PrizeMoneyForOnePoint;

                if (_raceService.EditRace(SelectedRace))
                {
                    ResetPage();
                }
               
                _ = GetRaces();
            }
        }

        private void DeleteRace()
        {
            if (SelectedRace != null)
            {
                _raceService.DeleteRace(SelectedRace.RaceId);

                _ = GetRaces();
            }
        }

        private void NewRacePointListSync()
        {
            _ = NewRacePointList();
        }

        private async Task NewRacePointList()
        {
            var newRacePointList = new List<RacePoint>();

            var settings = await _settingService.GetSettings();

            var maxRacePeople = int.Parse(settings
                .Where(s => s.Description == SettingsNames.GeneratedRacerPeople)
                .FirstOrDefault()
                .Value);

            for (int i = 0; i < maxRacePeople; i++)
            {
                var newRacePoint = new RacePoint();
                newRacePoint.Position = i + 1;
                newRacePoint.Point = 0;
                newRacePointList.Add(newRacePoint);
            }

            RacePointList = newRacePointList;
        }

        private void ResetPage()
        {
            RaceName = string.Empty;
            RaceLength = 0;
            RacePointList = new List<RacePoint>();
            PrizeMoneyForOnePoint = 0;
        }
    }
}
