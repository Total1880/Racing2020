using Racing.Model;
using System.Collections.Generic;

namespace Racing.Messages
{
    public class UpdateSeasonRankingMessage
    {
        public IList<RacerPerson> RacerPersonList { get; set; }
        public Race Race { get; set; }

        public UpdateSeasonRankingMessage(IList<RacerPerson> racerPersonList, Race race)
        {
            RacerPersonList = racerPersonList;
            Race = race;
        }
    }
}
