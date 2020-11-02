using System;
using System.Collections.Generic;
using System.Text;

namespace Racing.Settings
{
    public class SettingsDescription
    {
        public const string GeneratedRacerPeople = "GeneratedRacerPeople";
        public const string GeneratedTeams = "How many teams are generated";
        public const string NumberOfDivisions = "How many divisions are generated";
        public const string NumberOfTeamsPromoteRelegate = "Number Of Teams who Promote and Relegate Per Division";
        public const string GeneratedRacerPeoplePerTeam = "Generated Racers Per Team";
        public const string FactorPrizeMoneyPerDivisionTier = "if 2, a division get 1/2 prizemony of a division higher. If 3, its 1/3 and so on";
        public const string FactorFacilityUpgrade = "(BasicPriceFacilityUpgrade * current facility) * FactorFacilityUpgrade. So how higer this factor, how expensiver future updates.";
        public const string BasicPriceFacilityUpgrade = "Part of (BasicPriceFacilityUpgrade * current facility) * FactorFacilityUpgrade. Basic price of facility 1.";
        public const string YearsWhenFacilityDowngrade = "After how many years will a facility (probably) downgrade";
        public const string MinRacerStatsFacilityInfluence = "Per facility level, this number will be added to the update of the racer.";
    }
}
