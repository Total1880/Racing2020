using GalaSoft.MvvmLight;
using Racing.Messages;
using Racing.Model;
using System;
using System.Linq;
using System.Text;

namespace Racing.ViewModel
{
    public class NextRaceInfoPageViewModel : ViewModelBase
    {
        private Race _nextRace;
        private StringBuilder _raceInfoBuilder;
        private string _raceInfo;

        public string NextRaceInfo
        {
            get => _raceInfo;
            set
            {
                _raceInfo = value;
                RaisePropertyChanged();
            }
        }

        public NextRaceInfoPageViewModel()
        {
            _raceInfoBuilder = new StringBuilder();
            MessengerInstance.Register<NextRaceInfoMessage>(this, InitializeNextRace);
        }

        private void InitializeNextRace(NextRaceInfoMessage obj)
        {
            _nextRace = obj.Race;
            _nextRace.RacePointList = _nextRace.RacePointList.OrderBy(p => p.Position).ToList();
            _raceInfoBuilder = new StringBuilder();
            _raceInfoBuilder.Append($"Name: {_nextRace.Name}\n\n");
            _raceInfoBuilder.Append($"Length: { _nextRace.Length}\n\n");

            int counter = 0;

            do
            {
                _raceInfoBuilder.Append($"Position: {_nextRace.RacePointList[counter].Position} \tPoints: {_nextRace.RacePointList[counter].Point}\n");
                counter++;
            } while (_nextRace.RacePointList[counter] != null && _nextRace.RacePointList[counter].Point > 0);

            NextRaceInfo = _raceInfoBuilder.ToString();
        }
    }
}
