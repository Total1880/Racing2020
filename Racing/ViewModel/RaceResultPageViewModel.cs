using GalaSoft.MvvmLight;
using Racing.Messages;
using Racing.Model;
using Racing.Services.Interfaces;
using System.Collections.Generic;

namespace Racing.ViewModel
{
    public class RaceResultPageViewModel : ViewModelBase
    {
        private IRaceEngineService _raceEngineService;
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

        public RaceResultPageViewModel(IRaceEngineService raceEngineService)
        {
            _raceEngineService = raceEngineService;
            MessengerInstance.Register<RaceResultPageMessage>(this, GetRaceResult);
            MessengerInstance.Register<ResetSeasonMessage>(this, ResetRanking);
        }

        private void GetRaceResult(RaceResultPageMessage obj)
        {
            var newRacerPersonList = new List<RacerPerson>();
            foreach (var team in obj.TeamList)
            {
                newRacerPersonList.AddRange(team.RacerPeople);
            }
            _raceEngineService.Go(newRacerPersonList, obj.Race.Length);

            if (RacerPersonList != null)
                RacerPersonList.Clear();

            RacerPersonList = _raceEngineService.GetFinishRanking();
            MessengerInstance.Send(new UpdateSeasonRankingMessage(RacerPersonList, obj.Race, obj.DivisionId));
        }

        private void ResetRanking(ResetSeasonMessage obj)
        {
            RacerPersonList = new List<RacerPerson>();
        }
    }
}
