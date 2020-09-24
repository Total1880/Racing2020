using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Racing.Model
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public IList<RacerPerson> RacerPeople { get; set; }
        [NotMapped]
        public int Budget { get; set; }
        [NotMapped]
        public int TrainingFacility { get; set; }
        [NotMapped]
        public int YouthFacility { get; set; }
        [NotMapped]
        public FacilityUpgradePreference FacilityUpgradePreference { get; set; }
    }
}
