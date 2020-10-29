using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing.Messages;
using Racing.Model;
using Racing.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Racing.ViewModel
{
    public class CurrentGameSettingsPageViewModel : ViewModelBase
    {
        private IList<Team> _teamList;
        private ISaveGameSettingsService _saveGameSettingsService;
        private Team _selectedTeam;
        private RelayCommand _saveSettingsCommand;

        public IList<Team> TeamList
        {
            get => _teamList;
            set
            {
                _teamList = value;
                RaisePropertyChanged();
            }
        }

        public Team SelectedTeam
        {
            get => _selectedTeam;
            set
            {
                _selectedTeam = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand SaveSettingsCommand => _saveSettingsCommand ??= new RelayCommand(SaveSettings);

        public CurrentGameSettingsPageViewModel(ISaveGameSettingsService saveGameSettingsService)
        {
            _saveGameSettingsService = saveGameSettingsService;
            TeamList = new List<Team>();
            MessengerInstance.Register<OverviewRacerPersonsMessage>(this, OnOpenPage);
        }

        private void OnOpenPage(OverviewRacerPersonsMessage obj)
        {
            foreach (var division in obj.DivisionList)
            {
                foreach (var team in division.TeamList)
                {
                    TeamList.Add(team);
                }
            }

            SelectedTeam = TeamList.Where(t => t.TeamId == _saveGameSettingsService.GetPlayerTeamId()).FirstOrDefault();
        }

        private void SaveSettings()
        {
            _saveGameSettingsService.SaveGameSettings(SelectedTeam.TeamId);
        }
    }
}
