using Racing.Model;
using Racing.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Racing.Services
{
    public class SeasonEngineService : ISeasonEngineService
    {
        public IList<RacerSeasonRanking> RacerSeasonRankingList { get; set; }

        private void Convert(IList<RacerPerson> racerPersonList)
        {
            RacerSeasonRankingList = new List<RacerSeasonRanking>();

            foreach (var racerPerson in racerPersonList)
            {
                var newRacerSeasonRanking = new RacerSeasonRanking();

                newRacerSeasonRanking.Ability = racerPerson.Ability;
                newRacerSeasonRanking.FirstName = racerPerson.FirstName;
                newRacerSeasonRanking.LastName = racerPerson.LastName;
                newRacerSeasonRanking.Nation = racerPerson.Nation;
                newRacerSeasonRanking.Points = 0;
                newRacerSeasonRanking.RacerPersonId = racerPerson.RacerPersonId;

                RacerSeasonRankingList.Add(newRacerSeasonRanking);
            }
        }

        public void UpdateRanking(IList<RacerPerson> racerPersonList, Race race)
        {
            if (RacerSeasonRankingList == null)
            {
                Convert(racerPersonList);
            }

            for (int i = 0; i < racerPersonList.Count; i++)
            {
                RacerSeasonRankingList.Where(r => r.RacerPersonId == racerPersonList[i].RacerPersonId).FirstOrDefault().Points =
                    RacerSeasonRankingList.Where(r => r.RacerPersonId == racerPersonList[i].RacerPersonId).FirstOrDefault().Points +
                    race.RacePointList.Where(p => p.Position == i + 1).FirstOrDefault().Point;
            }
        }
    }
}
