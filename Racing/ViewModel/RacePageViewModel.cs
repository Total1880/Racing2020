using GalaSoft.MvvmLight;
using Racing.Messages;
using Racing.Model;
using System;
using System.Collections.Generic;

namespace Racing.ViewModel
{
    public class RacePageViewModel : ViewModelBase
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

        public RacePageViewModel()
        {
            MessengerInstance.Register<OverviewRacerPersonsMessage>(this, OnOpenRacePage);
        }

        private void OnOpenRacePage(OverviewRacerPersonsMessage obj)
        {
            RacerList = obj.RacerList;
        }
    }
}
