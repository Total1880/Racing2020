using Racing.Model;

namespace Racing.Messages
{
    class NextRaceInfoMessage
    {
        public Race Race{ get; set; }

        public NextRaceInfoMessage(Race race)
        {
            Race = race;
        }
    }
}
