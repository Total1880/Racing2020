using GalaSoft.MvvmLight;
using Racing.Messages;
using Racing.Model;
using Racing.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Racing.ViewModel
{
    public class TeamOverviewPageViewModel : ViewModelBase
    {
        private Team _selectedTeam;
        private readonly ISaveGameDivisionService _saveGameDivisionService;
        private IList<Division> _divisions;
        private ObservableCollection<Team> _teams;

        public Team SelectedTeam
        {
            get => _selectedTeam;
            set
            {
                _selectedTeam = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Team> Teams
        {
            get => _teams;
            set
            {
                _teams = value;
                RaisePropertyChanged();
            }
        }

        public TeamOverviewPageViewModel(ISaveGameDivisionService saveGameDivisionService)
        {
            _saveGameDivisionService = saveGameDivisionService;
            _divisions = _saveGameDivisionService.GetDivisions();

            MessengerInstance.Register<UpdateSeasonRankingMessage>(this, SelectDivision);
        }

        private void SelectDivision(UpdateSeasonRankingMessage obj)
        {
            Teams = new ObservableCollection<Team>(_divisions.Where(d => d.DivisionId == obj.DivisionId).FirstOrDefault().TeamList);
        }
    }
}
