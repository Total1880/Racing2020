using Racing.Model;
using System.Collections.Generic;

namespace Racing.Messages
{
    class RaceResultPageMessage
    {
        public IList<RacerPerson> RacerPersonList { get; set; }
        public int RaceLength { get; set; }

        public RaceResultPageMessage(IList<RacerPerson> racerPersonList, int raceLength)
        {
            RacerPersonList = racerPersonList;
            RaceLength = raceLength;
        }
    }
}
