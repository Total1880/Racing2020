using Racing.Model;
using System.Collections.Generic;

namespace Racing.Messages
{
    class ContinueGameMessage
    {
        public IList<Division> Divisions { get; set; }

        public ContinueGameMessage(IList<Division> divisions)
        {
            Divisions = divisions;
        }
    }
}
