using GalaSoft.MvvmLight;
using Racing.Messages;
using Racing.Model;
using System.Collections.Generic;

namespace Racing.ViewModel
{
    public class OverviewRacerPersonsViewModel : ViewModelBase
    {
        private IList<RacerPerson> _racerList;

        public IList<RacerPerson> RacerList
        {
            get => _racerList;
            set
            {
                _racerList = value;
                RaisePropertyChanged();
            }
        }

        public OverviewRacerPersonsViewModel(IList<RacerPerson> racerList)
        {
            MessengerInstance.Register<OverviewRacerPersonsMessage>(this, OnOpenOverviewRacerPersonsOverview);
            RacerList = racerList;
        }

        private void OnOpenOverviewRacerPersonsOverview(OverviewRacerPersonsMessage obj)
        {
            RacerList = obj.RacerList;
        }
    }
}
