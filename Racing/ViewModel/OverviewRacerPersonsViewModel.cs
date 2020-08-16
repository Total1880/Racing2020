using GalaSoft.MvvmLight;
using Racing.Messages;
using Racing.Model;
using System.Collections.Generic;

namespace Racing.ViewModel
{
    public class OverviewRacerPersonsViewModel : ViewModelBase
    {
        private IList<RacerPerson> _racerPersonList;

        public IList<RacerPerson> RacerPersonList
        {
            get => _racerPersonList;
            set
            {
                _racerPersonList = value;
                RaisePropertyChanged();
            }
        }

        public OverviewRacerPersonsViewModel(IList<RacerPerson> racerList)
        {
            MessengerInstance.Register<OverviewRacerPersonsMessage>(this, OnOpenOverviewRacerPersonsOverview);
            RacerPersonList = racerList;
        }

        private void OnOpenOverviewRacerPersonsOverview(OverviewRacerPersonsMessage obj)
        {
            RacerPersonList = obj.RacerList;
        }
    }
}
