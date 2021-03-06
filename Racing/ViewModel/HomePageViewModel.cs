﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing.Messages;
using Racing.Messages.WindowOpener;
using Racing.Model;
using Racing.Services.Interfaces;
using Racing.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.ViewModel
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly ISettingService _settingService;
        private readonly IDivisionInterface _divisionInterface;
        private readonly IRacerPersonService _racerPersonService;
        private IList<RacerPerson> _racerList;
        private IList<Division> _divisionList;
        private RelayCommand _openOverviewRacerPersonsCommand;
        private RelayCommand _startRaceCommand;
        private RelayCommand _startSeasonCommand;
        private RelayCommand _viewSettingsCommand;

        public RelayCommand OpenOverviewRacerPersonsCommand => _openOverviewRacerPersonsCommand ??= new RelayCommand(OpenOverviewRacerPersons);
        public RelayCommand StartRaceCommand => _startRaceCommand ??= new RelayCommand(StartRace);
        public RelayCommand StartSeasonCommand => _startSeasonCommand ??= new RelayCommand(StartSeason);
        public RelayCommand ViewSettingsCommand => _viewSettingsCommand ??= new RelayCommand(ViewSettings);

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

        public HomePageViewModel(ISettingService settingService, IDivisionInterface divisionInterface, IRacerPersonService racerPersonService)
        {
            _settingService = settingService;
            _divisionInterface = divisionInterface;
            _racerPersonService = racerPersonService;

            MessengerInstance.Register<NewGameMessage>(this, NewGame);
            MessengerInstance.Register<ContinueGameMessage>(this, ContinueGame);
        }


        private void NewGame(NewGameMessage obj)
        {
            _ = GenerateNewGame();
        }

        private void ContinueGame(ContinueGameMessage obj)
        {
            DivisionList = obj.Divisions;
        }

        private async Task GenerateNewGame()
        {
            var settingNumberOfNewRacersPerTeam = await _settingService.GetSettingByDescription(SettingsNames.GeneratedRacerPeoplePerTeam);
            var settingNumberOfNewTeams = await _settingService.GetSettingByDescription(SettingsNames.GeneratedTeams);
            var settingNumberOfDivisions = await _settingService.GetSettingByDescription(SettingsNames.NumberOfDivisions);

            DivisionList = await _divisionInterface.GenerateDivisions(int.Parse(settingNumberOfDivisions.Value), int.Parse(settingNumberOfNewTeams.Value), int.Parse(settingNumberOfNewRacersPerTeam.Value));

            foreach (var division in DivisionList)
            {
                foreach (var team in division.TeamList)
                {
                    team.RacerPeople = _racerPersonService.GenerateUniqueId(team.RacerPeople);
                }
            }
        }

        private void OpenOverviewRacerPersons()
        {
            if (DivisionList != null)
            {
                MessengerInstance.Send(new OpenOverviewRacerPersonsMessage());
                MessengerInstance.Send(new OverviewRacerPersonsMessage(DivisionList));
            }
        }

        private void StartRace()
        {
            //Nothing
        }

        private void StartSeason()
        {
            if (DivisionList != null)
            {
                MessengerInstance.Send(new OpenSeasonOverviewPageMessage());
                MessengerInstance.Send(new OverviewRacerPersonsMessage(DivisionList));
            }
        }

        private void ViewSettings()
        {
            if (DivisionList != null)
            {
                MessengerInstance.Send(new OpenCurrentGameSettingsMessage());
                MessengerInstance.Send(new OverviewRacerPersonsMessage(DivisionList));
            }
        }
    }
}
