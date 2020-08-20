namespace Racing.Model
{
    public class RacePoint
    {
        public int RacePointId { get; set; }
        public int Point { get; set; }
        public int Position { get; set; }
        public Race Race { get; set; }
    }
}
