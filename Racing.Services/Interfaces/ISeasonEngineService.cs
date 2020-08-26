using Racing.Model;
using System.Collections.Generic;

namespace Racing.Services.Interfaces
{
    public interface ISeasonEngineService
    {
        IDictionary<int, IList<RacerSeasonRanking>> DivisionRacerSeasonRankingList { get; set; }
        IDictionary<int, IList<TeamSeasonRanking>> DivisionTeamSeasonRankingList { get; set; }
        void UpdateRanking(IList<RacerPerson> racerPersonList, Race race, int divisionId);
        void ResetRanking();
    }
}
