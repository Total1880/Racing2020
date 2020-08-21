﻿using Racing.Model;
using System.Collections.Generic;

namespace Racing.Messages
{
    class RaceResultPageMessage
    {
        public IList<RacerPerson> RacerPersonList { get; set; }
        public Race Race { get; set; }

        public RaceResultPageMessage(IList<RacerPerson> racerPersonList, Race race)
        {
            RacerPersonList = racerPersonList;
            Race = race;
        }
    }
}
