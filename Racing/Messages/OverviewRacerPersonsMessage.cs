using Racing.Model;
using System.Collections.Generic;

namespace Racing.Messages
{
    class OverviewRacerPersonsMessage
    {
        public IList<RacerPerson> RacerList;

        public OverviewRacerPersonsMessage(IList<RacerPerson> racerList)
        {
            RacerList = racerList;
        }
    }
}
