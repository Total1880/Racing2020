using Racing.Model;
using Racing.Services.Interfaces;
using Racing.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Racing.Services
{
    public class SeasonEngineService : ISeasonEngineService
    {
        private readonly ISettingService _settingService;
        private Setting factorSetting;

        public IDictionary<int, IList<RacerSeasonRanking>> DivisionRacerSeasonRankingList { get; set; }
        public IDictionary<int, IList<TeamSeasonRanking>> DivisionTeamSeasonRankingList { get; set; }

        public SeasonEngineService(ISettingService settingService)
        {
            _settingService = settingService;
            _ = GetSetting();

            DivisionRacerSeasonRankingList = new Dictionary<int, IList<RacerSeasonRanking>>();
            DivisionTeamSeasonRankingList = new Dictionary<int, IList<TeamSeasonRanking>>();
        }

        private void Convert(IList<RacerPerson> racerPersonList, int divisionId)
        {
            var RacerSeasonRankingList = new List<RacerSeasonRanking>();
            var TeamSeasonRankingList = new List<TeamSeasonRanking>();

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
                    Age = racerPerson.Age,
                    DivisionId = divisionId
                };

                RacerSeasonRankingList.Add(newRacerSeasonRanking);

                if (!TeamSeasonRankingList.Any(t => t.TeamId == newRacerSeasonRanking.Team.TeamId))
                {
                    var newTeamSeasonRanking = new TeamSeasonRanking
                    {
                        Name = newRacerSeasonRanking.Team.Name,
                        TeamId = newRacerSeasonRanking.Team.TeamId,
                        DivisionId = divisionId
                    };

                    TeamSeasonRankingList.Add(newTeamSeasonRanking);
                }
            }

            DivisionRacerSeasonRankingList.Add(divisionId, RacerSeasonRankingList);
            DivisionTeamSeasonRankingList.Add(divisionId, TeamSeasonRankingList);
        }


        public void UpdateRanking(IList<RacerPerson> racerPersonList, Race race, Division division)
        {

            if (!DivisionRacerSeasonRankingList.ContainsKey(division.DivisionId))
            {
                Convert(racerPersonList, division.DivisionId);
            }

            for (int i = 0; i < racerPersonList.Count; i++)
            {

                if (race.RacePointList.Any(p => p.Position == i + 1 && p.Point != 0))
                {
                    var points = race.RacePointList.Where(p => p.Position == i + 1).FirstOrDefault().Point;

                    DivisionRacerSeasonRankingList[division.DivisionId].Where(r => r.RacerPersonId == racerPersonList[i].RacerPersonId).FirstOrDefault().Points =
                        DivisionRacerSeasonRankingList[division.DivisionId].Where(r => r.RacerPersonId == racerPersonList[i].RacerPersonId).FirstOrDefault().Points +
                        points;
                }

                DivisionRacerSeasonRankingList[division.DivisionId].Where(r => r.RacerPersonId == racerPersonList[i].RacerPersonId).FirstOrDefault().Positions =
                    DivisionRacerSeasonRankingList[division.DivisionId].Where(r => r.RacerPersonId == racerPersonList[i].RacerPersonId).FirstOrDefault().Positions + (i + 1);
            }

            foreach (var team in DivisionTeamSeasonRankingList[division.DivisionId])
            {
                team.Points = DivisionRacerSeasonRankingList[division.DivisionId].Where(r => r.Team.TeamId == team.TeamId).Sum(r => r.Points);
                team.Positions = DivisionRacerSeasonRankingList[division.DivisionId].Where(r => r.Team.TeamId == team.TeamId).Sum(r => r.Positions);
            }

            DivisionRacerSeasonRankingList[division.DivisionId] = DivisionRacerSeasonRankingList[division.DivisionId].Where(r => r.DivisionId == division.DivisionId).OrderByDescending(r => r.Points).ThenBy(r => r.Positions).ToList();
            DivisionTeamSeasonRankingList[division.DivisionId] = DivisionTeamSeasonRankingList[division.DivisionId].Where(r => r.DivisionId == division.DivisionId).OrderByDescending(t => t.Points).ThenBy(t => t.Positions).ToList();
        }

        public void ResetRanking()
        {
            DivisionRacerSeasonRankingList = new Dictionary<int, IList<RacerSeasonRanking>>();
            DivisionTeamSeasonRankingList = new Dictionary<int, IList<TeamSeasonRanking>>();
        }

        public void PromotionsAndRelegations(IList<Division> divisionList)
        {
            foreach (var division in divisionList)
            {
                if (division.Tier > 1)
                {
                    divisionList
                        .Where(d => d.Tier == division.Tier - 1)
                        .FirstOrDefault().TeamList.Add(division.TeamList.Where(t => t.TeamId == DivisionTeamSeasonRankingList[division.DivisionId][0].TeamId).FirstOrDefault());
                    division.TeamList.Remove(division.TeamList.Where(t => t.TeamId == DivisionTeamSeasonRankingList[division.DivisionId][0].TeamId).FirstOrDefault());
                }

                if (division.Tier != divisionList.Max(d => d.Tier))
                {
                    divisionList
                        .Where(d => d.Tier == division.Tier + 1)
                        .FirstOrDefault().TeamList.Add(division.TeamList.Where(t => t.TeamId == DivisionTeamSeasonRankingList[division.DivisionId].LastOrDefault().TeamId).FirstOrDefault());
                    division.TeamList.Remove(division.TeamList.Where(t => t.TeamId == DivisionTeamSeasonRankingList[division.DivisionId].LastOrDefault().TeamId).FirstOrDefault());
                }
            }
        }

        public IList<Team> UpdateFinances(IList<Team> teams, IList<RacerPerson> racerPersonList, Race race, int divisionTier)
        {
            if (race == null)
            {
                return null;
            }

            for (int i = 0; i < racerPersonList.Count; i++)
            {
                if (race.RacePointList.Any(p => p.Position == i + 1 && p.Point != 0))
                {
                    var points = race.RacePointList.Where(p => p.Position == i + 1).FirstOrDefault().Point;

                    if (divisionTier == 1)
                    {
                        teams.Where(t => t.TeamId == racerPersonList[i].Team.TeamId).FirstOrDefault().Budget += (points * race.PrizeMoneyForOnePoint);

                    }
                    else
                    {
                        teams.Where(t => t.TeamId == racerPersonList[i].Team.TeamId).FirstOrDefault().Budget += (points * race.PrizeMoneyForOnePoint) / ((divisionTier - 1) * int.Parse(factorSetting.Value));
                    }
                }
                else
                {
                    break;
                }
            }

            return teams;
        }

        private async Task GetSetting()
        {
           factorSetting = await _settingService.GetSettingByDescription(SettingsNames.FactorPrizeMoenyPerDivisionTier);
        }
    }
}
