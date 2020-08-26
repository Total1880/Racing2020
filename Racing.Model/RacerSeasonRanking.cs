namespace Racing.Model
{
    public class RacerSeasonRanking : RacerPerson
    {
        public int Points { get; set; }
        public int Positions { get; set; }
        public int DivisionId { get; set; }
    }
}
