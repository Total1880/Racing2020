using Racing.Model;
using System.Collections.Generic;

namespace Racing.Messages
{
    public class UpdateSeasonRankingMessage
    {
        public IList<RacerPerson> RacerPersonList { get; set; }
        public Race Race { get; set; }
        public int DivisionId { get; set; }

        public UpdateSeasonRankingMessage(IList<RacerPerson> racerPersonList, Race race, int divisionId)
        {
            RacerPersonList = racerPersonList;
            Race = race;
            DivisionId = divisionId;
        }

        public UpdateSeasonRankingMessage(int divisionId)
        {
            DivisionId = divisionId;
        }
    }
}
