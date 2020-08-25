using Racing.Model;
using Racing.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.Services
{
    public class DivisionService : IDivisionInterface
    {
        private readonly ITeamService _teamService;

        public DivisionService(ITeamService teamService)
        {
            _teamService = teamService;
        }
        public async Task<IList<Division>> GenerateDivisions(int numberOfDivisions, int numberOfNewTeams, int numberOfNewRacersPerTeam)
        {
            var newDivisionList = new List<Division>();

            for (int i = 1; i <= numberOfDivisions; i++)
            {
                var newDivision = new Division
                {
                    DivisionId = i,
                    Name = "Division " + i,
                    TeamList = await _teamService.GenerateTeams(numberOfNewRacersPerTeam, numberOfNewTeams)
                };

                newDivisionList.Add(newDivision);
            }

            return newDivisionList;
        }
    }
}
