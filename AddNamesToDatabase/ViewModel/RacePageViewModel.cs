using AddNamesToDatabase.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AddNamesToDatabase.ViewModel
{
    public class RacePageViewModel : ViewModelBase
    {
        private readonly IRaceService _raceService;
        private string _raceName;
        private int _raceLength;
        private ObservableCollection<Race> _raceList;
        private Race _selectedRace;
        private RelayCommand _addRaceCommand;
        private RelayCommand _editRaceCommand;

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
                }
            }
        }

        public RelayCommand AddRaceCommand => _addRaceCommand ??= new RelayCommand(AddRace);
        public RelayCommand EditRaceCommand => _editRaceCommand ??= new RelayCommand(EditRace);

        public RacePageViewModel(IRaceService raceService)
        {
            _raceService = raceService;
            _ = GetRaces();
        }

        public async Task GetRaces()
        {
            RaceList = new ObservableCollection<Race>(await _raceService.GetRaces());
        }

        private void AddRace()
        {
            var newRace = new Race { Name = RaceName, Length = RaceLength };

            if (newRace.Length > 10 && newRace.Length <= 1000)
            {
                _raceService.CreateRace(newRace);
            }
        }

        private void EditRace()
        {
            if (SelectedRace != null)
            {
                SelectedRace.Name = RaceName;
                SelectedRace.Length = RaceLength;

                _raceService.EditRace(SelectedRace);

                _ = GetRaces();
            }
        }
    }
}
