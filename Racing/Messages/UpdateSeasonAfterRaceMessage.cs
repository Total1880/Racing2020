using Racing.Model;
using System.Collections.Generic;

namespace Racing.Messages
{
    public class UpdateSeasonAfterRaceMessage
    {
        public IList<RacerPerson> RacerPersonList { get; set; }
        public Race Race { get; set; }
        public Division Division { get; set; }

        public UpdateSeasonAfterRaceMessage(IList<RacerPerson> racerPersonList, Race race, Division division)
        {
            RacerPersonList = racerPersonList;
            Race = race;
            Division = division;
        }

        public UpdateSeasonAfterRaceMessage(Division division)
        {
            Division = division;
        }
    }
}
