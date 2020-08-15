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
        private int _raceLength;
        private bool _validRaceLength = false;

        public IList<RacerPerson> RacerList
        {
            get => _racerList;
            set
            {
                _racerList = value;
                RaisePropertyChanged();
            }
        }

        public int RaceLength
        {
            get => _raceLength;
            set 
            {
                if (value > 10 && value <= 1000)
                {
                    _raceLength = value;
                    InputBoxRaceLengthEnabled = false;
                }
            }
        }

        public bool InputBoxRaceLengthEnabled
        {
            get => !_validRaceLength;
            set
            {
                _validRaceLength = !value;
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
