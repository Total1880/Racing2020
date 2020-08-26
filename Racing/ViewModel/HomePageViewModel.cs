using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing.Messages;
using Racing.Messages.WindowOpener;
using Racing.Model;
using Racing.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.ViewModel
{
    public class HomePageViewModel : ViewModelBase
    {
        private ISettingService _settingService;
        private IDivisionInterface _divisionInterface;
        private IList<RacerPerson> _racerList;
        private IList<Division> _divisionList;
        private RelayCommand _openOverviewRacerPersonsCommand;
        private RelayCommand _startRaceCommand;
        private RelayCommand _startSeasonCommand;

        public RelayCommand OpenOverviewRacerPersonsCommand => _openOverviewRacerPersonsCommand ??= new RelayCommand(OpenOverviewRacerPersons);
        public RelayCommand StartRaceCommand => _startRaceCommand ??= new RelayCommand(StartRace);
        public RelayCommand StartSeasonCommand => _startSeasonCommand ??= new RelayCommand(StartSeason);

        public IList<RacerPerson> RacerList
        {
            get => _racerList;
            set
            {
                _racerList = value;
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

        public HomePageViewModel(ISettingService settingService, IDivisionInterface divisionInterface)
        {
            _settingService = settingService;
            _divisionInterface = divisionInterface;

            _ = GenerateNewGame();
        }

        private async Task GenerateNewGame()
        {
            var settingNumberOfNewRacersPerTeam = await _settingService.GetSettingByDescription(SettingsNames.GeneratedRacerPeoplePerTeam);
            var settingNumberOfNewTeams = await _settingService.GetSettingByDescription(SettingsNames.GeneratedTeams);
            var settingNumberOfDivisions = await _settingService.GetSettingByDescription(SettingsNames.NumberOfDivisions);

            DivisionList = await _divisionInterface.GenerateDivisions(int.Parse(settingNumberOfDivisions.Value), int.Parse(settingNumberOfNewTeams.Value), int.Parse(settingNumberOfNewRacersPerTeam.Value));
        }

        private void OpenOverviewRacerPersons()
        {
            MessengerInstance.Send(new OpenOverviewRacerPersonsMessage());
            MessengerInstance.Send(new OverviewRacerPersonsMessage(DivisionList));
        }

        private void StartRace()
        {
            if (RacerList != null)
            {
                MessengerInstance.Send(new OpenRacePageMessage());
                MessengerInstance.Send(new OverviewRacerPersonsMessage(DivisionList));
            }
        }

        private void StartSeason()
        {
            if (DivisionList != null)
            {
                MessengerInstance.Send(new OpenSeasonOverviewPageMessage());
                MessengerInstance.Send(new OverviewRacerPersonsMessage(DivisionList));
            }
        }
    }
}
