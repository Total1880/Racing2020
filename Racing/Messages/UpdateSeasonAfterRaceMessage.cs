using Racing.Model;
using System.Collections.Generic;

namespace Racing.Messages
{
    public class UpdateSeasonAfterRaceMessage
    {
        public IList<RacerPerson> RacerPersonList { get; set; }
        public Race Race { get; set; }
        public Division Division { get; set; }
        public bool UserViewedRace { get; set; }

        public UpdateSeasonAfterRaceMessage(IList<RacerPerson> racerPersonList, Race race, Division division, bool userViewedRace)
        {
            RacerPersonList = racerPersonList;
            Race = race;
            Division = division;
            UserViewedRace = userViewedRace;
        }

        public UpdateSeasonAfterRaceMessage(Division division)
        {
            Division = division;
        }
    }
}
