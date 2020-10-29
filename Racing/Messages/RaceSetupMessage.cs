using Racing.Model;

namespace Racing.Messages
{
    class RaceSetupMessage
    {
        public Race Race { get; set; }
        public Division Division { get; set; }
        public int PlayerTeamId { get; set; }

        public RaceSetupMessage(Division division, Race race, int playerTeamId)
        {
            Race = race;
            Division = division;
            PlayerTeamId = playerTeamId;
        }
    }
}
