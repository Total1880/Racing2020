using Racing.Model;
using System.Collections.Generic;

namespace Racing.Messages
{
    public class ResetSeasonMessage
    {
        public IList<Division> DivisionList { get; set; }

        public ResetSeasonMessage(IList<Division> divisionList)
        {
            DivisionList = divisionList;
        }
    }
}
