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
        private IList<RacePart> _racePartList;
        private RacePoint _selectedRacePoint;
        private RacePart _selectedRacePart;

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
                    RacePartList = value.RacePartList;
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

        public IList<RacePart> RacePartList
        {
            get => _racePartList.OrderBy(p => p.Start).ToList();
            set
            {
                _racePartList = value;
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

        public RacePart SelectedRacePart
        {
            get => _selectedRacePart;
            set
            {
                _selectedRacePart = value;

                if (_selectedRacePart != null && !SelectedRace.RacePartList.Contains(_selectedRacePart))
                {
                    SelectedRace.RacePartList.Add(_selectedRacePart);
                }
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
            var newRace = new Race { Name = RaceName, Length = RaceLength, RacePointList = RacePointList, PrizeMoneyForOnePoint = PrizeMoneyForOnePoint, RacePartList = RacePartList };

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
                SelectedRace.RacePartList = RacePartList;
                SelectedRace.PrizeMoneyForOnePoint = PrizeMoneyForOnePoint;

                CheckRaceParts(SelectedRace.RacePartList);

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
            RacePartList = new List<RacePart>();
            PrizeMoneyForOnePoint = 0;
        }

        private void CheckRaceParts(IList<RacePart> racePartList)
        {
            racePartList = racePartList.OrderBy(r => r.Start).ToList();

            for (int i = 0; i < racePartList.Count; i++)
            {
                if (i < racePartList.Count - 1 && racePartList[i].End >= racePartList[i + 1].Start)
                {
                    racePartList[i].End = racePartList[i + 1].Start - 1;
                }

                if (i > 0 && racePartList[i].Start <= racePartList[i - 1].End)
                {
                    racePartList[i].Start = racePartList[i - 1].End + 1;
                }
            }

            if (RacePartList[RacePartList.Count - 1].End != SelectedRace.Length)
            {
                RacePartList[RacePartList.Count - 1].End = SelectedRace.Length;
            }
        }
    }
}
