using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing.Messages;
using Racing.Messages.WindowOpener;
using Racing.Model;
using Racing.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.ViewModel
{
    public class HomePageViewModel : ViewModelBase
    {
        private IRacerPersonService _racerPersonService;
        private ISettingService _settingService;
        private IList<RacerPerson> _racerList;
        private int _numberOfNewRacers = 20;
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

        public HomePageViewModel(IRacerPersonService racerPersonService, ISettingService settingService)
        {
            _racerPersonService = racerPersonService;
            _settingService = settingService;

            _ = GenerateNewGame();
        }

        private async Task GenerateNewGame()
        {
            var settingNumberOfNewRacers = await _settingService.GetSettingByDescription(SettingsNames.GeneratedRacerPeople);
            var settingNumberOfNewTeams = await _settingService.GetSettingByDescription(SettingsNames.GeneratedTeams);

            RacerList = await _racerPersonService.GenerateRacerPeople(int.Parse(settingNumberOfNewRacers.Value), int.Parse(settingNumberOfNewTeams.Value));
        }

        private void OpenOverviewRacerPersons()
        {
            MessengerInstance.Send(new OpenOverviewRacerPersonsMessage());
            MessengerInstance.Send(new OverviewRacerPersonsMessage(RacerList));
        }

        private void StartRace()
        {
            if (RacerList != null)
            {
                MessengerInstance.Send(new OpenRacePageMessage());
                MessengerInstance.Send(new OverviewRacerPersonsMessage(RacerList));
            }
        }

        private void StartSeason()
        {
            if (RacerList != null)
            {
                MessengerInstance.Send(new OpenSeasonOverviewPageMessage());
                MessengerInstance.Send(new OverviewRacerPersonsMessage(RacerList));
            }
        }
    }
}
