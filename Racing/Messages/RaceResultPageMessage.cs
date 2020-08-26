using Racing.Model;
using System.Collections.Generic;

namespace Racing.Messages
{
    class RaceResultPageMessage
    {
        public IList<Team> TeamList { get; set; }
        public Race Race { get; set; }
        public int DivisionId { get; set; }

        public RaceResultPageMessage(IList<Team> teamList, Race race, int divisionId)
        {
            TeamList = teamList;
            Race = race;
            DivisionId = divisionId;
        }
    }
}
