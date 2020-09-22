using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.Services.Interfaces
{
    public interface ISeasonEngineService
    {
        IDictionary<int, IList<RacerSeasonRanking>> DivisionRacerSeasonRankingList { get; set; }
        IDictionary<int, IList<TeamSeasonRanking>> DivisionTeamSeasonRankingList { get; set; }
        void UpdateRanking(IList<RacerPerson> racerPersonList, Race race, Division division);
        void ResetRanking();
        void PromotionsAndRelegations(IList<Division> divisionList);
        IList<Team> UpdateFinances(IList<Team> teams, IList<RacerPerson> racerPersonList, Race race, int divisionTier);
    }
}
