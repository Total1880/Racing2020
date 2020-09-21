using Racing.Model;
using System.Collections.Generic;

namespace Racing.Messages
{
    public class UpdateSeasonRankingMessage
    {
        public IList<RacerPerson> RacerPersonList { get; set; }
        public Race Race { get; set; }
        public Division Division { get; set; }

        public UpdateSeasonRankingMessage(IList<RacerPerson> racerPersonList, Race race, Division division)
        {
            RacerPersonList = racerPersonList;
            Race = race;
            Division = division;
        }

        public UpdateSeasonRankingMessage(Division division)
        {
            Division = division;
        }
    }
}
