using AddNamesToDatabase.Services.Interfaces;
using GalaSoft.MvvmLight;
using Racing.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AddNamesToDatabase.ViewModel
{
    public class TeamPageViewModel : ViewModelBase
    {
        private readonly ITeamService _teamService;
        private IList<Team> _teamList;

        public IList<Team> TeamList
        {
            get => _teamList;
            set
            {
                _teamList = value;
                RaisePropertyChanged();
            }
        }

        public TeamPageViewModel(ITeamService teamService)
        {
            _teamService = teamService;
            _ = GetTeams();
        }

        private async Task GetTeams()
        {
            TeamList = new ObservableCollection<Team>(await _teamService.GetTeams()) ;
        }
    }
}
