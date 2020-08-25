using Racing.Model;
using System.Collections.Generic;

namespace Racing.Messages
{
    class OverviewRacerPersonsMessage
    {
        public IList<Division> DivisionList;

        public OverviewRacerPersonsMessage(IList<Division> divisionList)
        {
            DivisionList = divisionList;
        }
    }
}
