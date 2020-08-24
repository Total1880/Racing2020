using Racing.Model;
using System.Collections.Generic;

namespace Racing.Services.Interfaces
{
    public interface ISeasonEngineService
    {
        IList<RacerSeasonRanking> RacerSeasonRankingList { get; set; }
        IList<TeamSeasonRanking> TeamSeasonRankingList { get; set; }
        void UpdateRanking(IList<RacerPerson> racerPersonList, Race race);
        void ResetRanking();
    }
}
