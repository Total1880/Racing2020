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
        }

        private void UpdateSeasonRanking(UpdateSeasonRankingMessage obj)
        {
            _seasonEngineService.UpdateRanking(obj.RacerPersonList, obj.Race, obj.DivisionId);
            RacerSeasonRankingList = _seasonEngineService.DivisionRacerSeasonRankingList[obj.DivisionId].Where(r => r.DivisionId == obj.DivisionId).OrderByDescending(r => r.Points).ThenBy(r => r.Positions).ToList();
            TeamSeasonRankingList = _seasonEngineService.DivisionTeamSeasonRankingList[obj.DivisionId].Where(r => r.DivisionId == obj.DivisionId).OrderByDescending(t => t.Points).ThenBy(t => t.Positions).ToList();
            MessengerInstance.Send(new UpdateJerseyMessage(RacerSeasonRankingList[0].RacerPersonId));
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
