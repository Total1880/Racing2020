using GalaSoft.MvvmLight;
using Racing.Messages;
using Racing.Model;
using Racing.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace Racing.ViewModel
{
    public class RaceResultPageViewModel : ViewModelBase
    {
        private readonly IRaceEngineService _raceEngineService;
        private IList<RacerPerson> _racerPersonList;
        private Dictionary<int, IList<RacerPerson>> _latestResultsPerDivision;
        private Race _currentRace;
        private StringBuilder _raceInfoBuilder;
        private string _raceInfo;

        public IList<RacerPerson> RacerPersonList
        {
            get => _racerPersonList;
            set
            {
                _racerPersonList = value;
                RaisePropertyChanged();
            }
        }

        public string RaceInfo
        {
            get => _raceInfo;
            set
            {
                _raceInfo = value;
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

            _raceEngineService.Go(_latestResultsPerDivision[obj.Division.DivisionId], obj.Race);

            if (_latestResultsPerDivision[obj.Division.DivisionId] != null)
                _latestResultsPerDivision[obj.Division.DivisionId].Clear();

            _latestResultsPerDivision[obj.Division.DivisionId] = _raceEngineService.GetFinishRanking();

            MessengerInstance.Send(new UpdateSeasonAfterRaceMessage(_latestResultsPerDivision[obj.Division.DivisionId], obj.Race, obj.Division ));
            ShowRace(obj.Race);

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

        private void ShowRace(Race race)
        {
            _currentRace = race;
            _currentRace.RacePointList = _currentRace.RacePointList.OrderBy(p => p.Position).ToList();
            _raceInfoBuilder = new StringBuilder();
            _raceInfoBuilder.Append($"Name: {_currentRace.Name}\n\n");
            _raceInfoBuilder.Append($"Length: { _currentRace.Length}\n\n");

            int counter = 0;

            do
            {
                _raceInfoBuilder.Append($"Position: {_currentRace.RacePointList[counter].Position} \tPoints: {_currentRace.RacePointList[counter].Point}\n");
                counter++;
            } while (counter < _currentRace.RacePointList.Count && _currentRace.RacePointList[counter].Point > 0);

            RaceInfo = _raceInfoBuilder.ToString();
        }
    }
}
