using GalaSoft.MvvmLight;
using Racing.Model;
using Racing.Services.Interfaces;

namespace Racing.ViewModel
{
    public class TeamOverviewPageViewModel : ViewModelBase
    {
        private Team _selectedTeam;
        private readonly ITeamService _teamService;

        public Team SelectedTeam
        {
            get => _selectedTeam;
            set
            {
                _selectedTeam = value;
                RaisePropertyChanged();
            }
        }

        public TeamOverviewPageViewModel(ITeamService teamService)
        {
            _teamService = teamService;
            
        }
    }
}
