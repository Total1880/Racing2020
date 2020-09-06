using GalaSoft.MvvmLight;
using Racing.Messages;
using Racing.Model;
using System.Collections.Generic;

namespace Racing.ViewModel
{
    public class OverviewRacerPersonsViewModel : ViewModelBase
    {
        private IList<Division> _divisionList;
        private IList<RacerPerson> _racerPersonList;
        private Division _selectedDivision;

        public IList<RacerPerson> RacerPersonList
        {
            get => _racerPersonList;
            set
            {
                _racerPersonList = value;
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

        public Division SelectedDivision
        {
            get => _selectedDivision;
            set
            {
                if (value != null)
                {
                    _selectedDivision = value;
                    RaisePropertyChanged();
                    RacerPersonList = new List<RacerPerson>();
                    foreach (var team in value.TeamList)
                    {
                        foreach (var racerPerson in team.RacerPeople)
                        {
                            racerPerson.Team = team;
                            RacerPersonList.Add(racerPerson);
                        }
                    }
                }
            }
        }

        public OverviewRacerPersonsViewModel(IList<RacerPerson> racerList)
        {
            MessengerInstance.Register<OverviewRacerPersonsMessage>(this, OnOpenOverviewRacerPersonsOverview);
            RacerPersonList = racerList;
        }

        private void OnOpenOverviewRacerPersonsOverview(OverviewRacerPersonsMessage obj)
        {
            DivisionList = obj.DivisionList;
            SelectedDivision = DivisionList[0];
        }
    }
}
