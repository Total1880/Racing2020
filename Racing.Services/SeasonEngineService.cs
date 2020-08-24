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
                newRacerSeasonRanking.Positions = 0;
                newRacerSeasonRanking.RacerPersonId = racerPerson.RacerPersonId;
                newRacerSeasonRanking.Team = racerPerson.Team;

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
                if (race.RacePointList.Any(p => p.Position == i + 1))
                {
                    RacerSeasonRankingList.Where(r => r.RacerPersonId == racerPersonList[i].RacerPersonId).FirstOrDefault().Points =
                        RacerSeasonRankingList.Where(r => r.RacerPersonId == racerPersonList[i].RacerPersonId).FirstOrDefault().Points +
                        race.RacePointList.Where(p => p.Position == i + 1).FirstOrDefault().Point;
                }

                RacerSeasonRankingList.Where(r => r.RacerPersonId == racerPersonList[i].RacerPersonId).FirstOrDefault().Positions =
                    RacerSeasonRankingList.Where(r => r.RacerPersonId == racerPersonList[i].RacerPersonId).FirstOrDefault().Positions + (i + 1);
            }
        }

        public void ResetRanking()
        {
            if (RacerSeasonRankingList == null)
                return;

            foreach (var racerSeason in RacerSeasonRankingList)
            {
                racerSeason.Points = 0;
                racerSeason.Positions = 0;
            }
        }
    }
}
