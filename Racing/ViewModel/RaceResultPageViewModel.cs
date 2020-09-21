using GalaSoft.MvvmLight;
using Racing.Messages;
using Racing.Model;
using Racing.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace Racing.ViewModel
{
    public class RaceResultPageViewModel : ViewModelBase
    {
        private readonly IRaceEngineService _raceEngineService;
        private IList<RacerPerson> _racerPersonList;
        private Dictionary<int, IList<RacerPerson>> _latestResultsPerDivision;

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
            _latestResultsPerDivision = new Dictionary<int, IList<RacerPerson>>();
        }

        private void GetRaceResult(RaceResultPageMessage obj)
        {
            if (obj.Race != null)
            {
                RunRaceResult(obj);
            }
            else
            {
                ViewRaceResult(obj);
            }
        }

        private void RunRaceResult(RaceResultPageMessage obj)
        {
            if (_latestResultsPerDivision.Any(d => d.Key == obj.Division.DivisionId))
                _latestResultsPerDivision.Remove(obj.Division.DivisionId);

            _latestResultsPerDivision[obj.Division.DivisionId] = new List<RacerPerson>();
            foreach (var team in obj.TeamList)
            {
                foreach (var racerPerson in team.RacerPeople)
                {
                    _latestResultsPerDivision[obj.Division.DivisionId].Add(racerPerson);
                }
            }

            _raceEngineService.Go(_latestResultsPerDivision[obj.Division.DivisionId], obj.Race.Length);

            if (_latestResultsPerDivision[obj.Division.DivisionId] != null)
                _latestResultsPerDivision[obj.Division.DivisionId].Clear();

            _latestResultsPerDivision[obj.Division.DivisionId] = _raceEngineService.GetFinishRanking();

            MessengerInstance.Send(new UpdateSeasonRankingMessage(_latestResultsPerDivision[obj.Division.DivisionId], obj.Race, obj.Division ));

            RacerPersonList = _latestResultsPerDivision[obj.Division.DivisionId];
        }

        private void ViewRaceResult(RaceResultPageMessage obj)
        {
            if (_latestResultsPerDivision.ContainsKey(obj.Division.DivisionId))
            {
                RacerPersonList = _latestResultsPerDivision[obj.Division.DivisionId];
            }
        }

        private void ResetRanking(ResetSeasonMessage obj)
        {
            RacerPersonList = new List<RacerPerson>();
        }
    }
}
