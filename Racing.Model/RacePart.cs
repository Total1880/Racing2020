using Racing.Model.Enums;

namespace Racing.Model
{
    public class RacePart
    {
        public int RacePartId { get; set; }
        public RacePartEnum Part { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public Race Race { get; set; }
    }
}
