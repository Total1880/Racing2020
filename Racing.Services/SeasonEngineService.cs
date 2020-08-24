using Racing.Model;
using Racing.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Racing.Services
{
    public class SeasonEngineService : ISeasonEngineService
    {
        public IList<RacerSeasonRanking> RacerSeasonRankingList { get; set; }
        public IList<TeamSeasonRanking> TeamSeasonRankingList { get; set; }

        private void Convert(IList<RacerPerson> racerPersonList)
        {
            RacerSeasonRankingList = new List<RacerSeasonRanking>();
            TeamSeasonRankingList = new List<TeamSeasonRanking>();

            foreach (var racerPerson in racerPersonList)
            {
                var newRacerSeasonRanking = new RacerSeasonRanking
                {
                    Ability = racerPerson.Ability,
                    FirstName = racerPerson.FirstName,
                    LastName = racerPerson.LastName,
                    Nation = racerPerson.Nation,
                    RacerPersonId = racerPerson.RacerPersonId,
                    Team = racerPerson.Team,
                    PotentialAbility = racerPerson.PotentialAbility,
                    Age = racerPerson.Age
                };

                RacerSeasonRankingList.Add(newRacerSeasonRanking);

                if (!TeamSeasonRankingList.Any(t => t.TeamId == newRacerSeasonRanking.Team.TeamId))
                {
                    var newTeamSeasonRanking = new TeamSeasonRanking
                    {
                        Name = newRacerSeasonRanking.Team.Name,
                        TeamId = newRacerSeasonRanking.Team.TeamId
                    };

                    TeamSeasonRankingList.Add(newTeamSeasonRanking);
                }
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

            foreach (var team in TeamSeasonRankingList)
            {
                team.Points = RacerSeasonRankingList.Where(r => r.Team.TeamId == team.TeamId).Sum(r => r.Points);
                team.Positions = RacerSeasonRankingList.Where(r => r.Team.TeamId == team.TeamId).Sum(r => r.Positions);
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
