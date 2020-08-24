using AddNamesToDatabase.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AddNamesToDatabase.ViewModel
{
    public class TeamPageViewModel : ViewModelBase
    {
        private readonly ITeamService _teamService;
        private IList<Team> _teamList;
        private string _teamName;
        private Team _selectedTeam;
        private RelayCommand _addTeamCommand;
        private RelayCommand _editTeamCommand;
        private RelayCommand _deleteTeamCommand;

        public IList<Team> TeamList
        {
            get => _teamList;
            set
            {
                _teamList = value;
                RaisePropertyChanged();
            }
        }

        public string TeamName
        {
            get => _teamName;
            set
            {
                _teamName = value;
                RaisePropertyChanged();
            }
        }

        public Team SelectedTeam
        {
            get => _selectedTeam;
            set
            {
                if (value != null)
                {
                    _selectedTeam = value;
                    TeamName = value.Name;
                }
            }
        }

        public RelayCommand AddTeamCommand => _addTeamCommand ??= new RelayCommand(AddTeam);
        public RelayCommand EditTeamCommand => _editTeamCommand ??= new RelayCommand(EditTeam);
        public RelayCommand DeleteTeamCommand => _deleteTeamCommand ??= new RelayCommand(DeleteTeam);

        public TeamPageViewModel(ITeamService teamService)
        {
            _teamService = teamService;
            _ = GetTeams();
        }

        private async Task GetTeams()
        {
            TeamList = new ObservableCollection<Team>(await _teamService.GetTeams()) ;
        }

        private void AddTeam()
        {
            if (!string.IsNullOrWhiteSpace(TeamName))
            {
                var newTeam = new Team { Name = TeamName };
                _teamService.CreateTeam(newTeam);
                _ = GetTeams();
            }
        }

        private void EditTeam()
        {
            if (!string.IsNullOrWhiteSpace(TeamName))
            {
                SelectedTeam.Name = TeamName;
                _teamService.EditTeam(SelectedTeam);
                _ = GetTeams();
            }
        }

        private void DeleteTeam()
        {
            if (SelectedTeam != null)
            {
                _teamService.DeleteTeam(SelectedTeam.TeamId);
                _ = GetTeams();
                TeamName = string.Empty;
            }
        }
    }
}
