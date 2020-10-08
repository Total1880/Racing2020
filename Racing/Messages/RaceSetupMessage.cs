using Racing.Model;

namespace Racing.Messages
{
    class RaceSetupMessage
    {
        public Race Race { get; set; }
        public Division Division { get; set; }

        public RaceSetupMessage(Division division, Race race)
        {
            Race = race;
            Division = division;
        }
    }
}
