using GalaSoft.MvvmLight;
using Racing.Messages;
using Racing.Model;
using Racing.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Racing.ViewModel
{
    public class SeasonRankingPageViewModel : ViewModelBase
    {
        private ISeasonEngineService _seasonEngineService;
        private IList<RacerSeasonRanking> _racerSeasonRankingList;
        private IList<TeamSeasonRanking> _teamSeasonRankings;
        private Dictionary<int, IList<RacerSeasonRanking>> _rankingPerDivisionPerRacer;
        private Dictionary<int, IList<TeamSeasonRanking>> _rankingPerDivisionPerTeam;

        public IList<RacerSeasonRanking> RacerSeasonRankingList
        {
            get => _racerSeasonRankingList;
            set
            {
                _racerSeasonRankingList = value;
                RaisePropertyChanged();
            }
        }

        public IList<TeamSeasonRanking> TeamSeasonRankingList
        {
            get => _teamSeasonRankings;
            set
            {
                _teamSeasonRankings = value;
                RaisePropertyChanged();
            }
        }

        public SeasonRankingPageViewModel(ISeasonEngineService seasonEngineService)
        {
            _seasonEngineService = seasonEngineService;
            MessengerInstance.Register<UpdateSeasonRankingMessage>(this, UpdateSeasonRanking);
            MessengerInstance.Register<ResetSeasonMessage>(this, ResetSeasonRanking);
            RacerSeasonRankingList = new List<RacerSeasonRanking>();
            TeamSeasonRankingList = new List<TeamSeasonRanking>();
            _rankingPerDivisionPerRacer = new Dictionary<int, IList<RacerSeasonRanking>>();
            _rankingPerDivisionPerTeam = new Dictionary<int, IList<TeamSeasonRanking>>();
        }

        private void UpdateSeasonRanking(UpdateSeasonRankingMessage obj)
        {
            if (obj.Race != null)
            {
                UpdateSeasonRankingWithRace(obj);
            }
            else
            {
                UpdateSeasonRankingOnlyDivision(obj);
            }
        }

        private void UpdateSeasonRankingWithRace(UpdateSeasonRankingMessage obj)
        {
            _seasonEngineService.UpdateRanking(obj.RacerPersonList, obj.Race, obj.DivisionId);
            _rankingPerDivisionPerRacer[obj.DivisionId] = new List<RacerSeasonRanking>();
            _rankingPerDivisionPerTeam[obj.DivisionId] = new List<TeamSeasonRanking>();
            RacerSeasonRankingList = new List<RacerSeasonRanking>();
            TeamSeasonRankingList = new List<TeamSeasonRanking>();

            if (_rankingPerDivisionPerRacer.Any(d => d.Key == obj.DivisionId))
                RacerSeasonRankingList = _seasonEngineService.DivisionRacerSeasonRankingList[obj.DivisionId];

            if (_rankingPerDivisionPerTeam.Any(d => d.Key == obj.DivisionId))
                TeamSeasonRankingList = _seasonEngineService.DivisionTeamSeasonRankingList[obj.DivisionId];

            MessengerInstance.Send(new UpdateJerseyMessage(RacerSeasonRankingList[0].RacerPersonId, obj.DivisionId));
        }

        private void UpdateSeasonRankingOnlyDivision(UpdateSeasonRankingMessage obj)
        {
            RacerSeasonRankingList = new List<RacerSeasonRanking>();
            TeamSeasonRankingList = new List<TeamSeasonRanking>();

            if (_seasonEngineService.DivisionRacerSeasonRankingList.Any(d => d.Key == obj.DivisionId))
            {
                RacerSeasonRankingList = _seasonEngineService.DivisionRacerSeasonRankingList[obj.DivisionId];
            }

            if (_seasonEngineService.DivisionTeamSeasonRankingList.Any(d => d.Key == obj.DivisionId))
            {
                TeamSeasonRankingList = _seasonEngineService.DivisionTeamSeasonRankingList[obj.DivisionId];
            }
        }

        private void ResetSeasonRanking(ResetSeasonMessage obj)
        {
            _seasonEngineService.PromotionsAndRelegations(obj.DivisionList);
            _seasonEngineService.ResetRanking();
            RacerSeasonRankingList = new List<RacerSeasonRanking>();
            TeamSeasonRankingList = new List<TeamSeasonRanking>();
        }
    }
}
