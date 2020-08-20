using System.Collections.Generic;

namespace Racing.Model
{
    public class Race
    {
        public int RaceId { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public IList<RacePoint> RacePointList { get; set; }
    }
}
