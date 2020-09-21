using Racing.Model;
using System.Collections.Generic;

namespace Racing.Messages
{
    class RaceResultPageMessage
    {
        public IList<Team> TeamList { get; set; }
        public Race Race { get; set; }
        public Division Division { get; set; }

        public RaceResultPageMessage(IList<Team> teamList, Race race, Division division)
        {
            TeamList = teamList;
            Race = race;
            Division = division;
        }
        public RaceResultPageMessage(Division division)
        {
            Division = division;
        }
    }
}
