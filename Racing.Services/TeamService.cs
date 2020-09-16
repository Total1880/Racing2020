using Racing.Model;
using Racing.Repositories;
using Racing.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.Services
{
    public class TeamService : ITeamService
    {
        private readonly IRacerPersonService _racerPersonService;
        private INamesRepository<FirstNames> _firstNamesRepository;
        private INamesRepository<LastNames> _lastNamesRepository;
        private IRepository<Team> _teamRepository;
        private readonly Random _random = new Random();
        private int teamIndex = 0;
        private int _minAge = 18;
        private int _maxAge = 40;
        IList<Team> teams;


        public TeamService(IRacerPersonService racerPersonService, INamesRepository<FirstNames> firstNamesRepository, INamesRepository<LastNames> lastNamesRepository, IRepository<Team> teamRepository)
        {
            _racerPersonService = racerPersonService;
            _firstNamesRepository = firstNamesRepository;
            _lastNamesRepository = lastNamesRepository;
            _teamRepository = teamRepository;
            _ = GetTeams();
        }

        private async Task GetTeams()
        {
            teams = await _teamRepository.Get();
        }

        public async Task<IList<Team>> GenerateTeams(int numberOfPeoplePerTeam, int numberOfTeams)
        {
            var teamList = new List<Team>();

            for (int i = 0; i < numberOfTeams; i++)
            {
                teamList.Add(teams[teamIndex]);

                var firstNames = await _firstNamesRepository.GenerateNames(numberOfPeoplePerTeam);
                var lastNames = await _lastNamesRepository.GenerateNames(numberOfPeoplePerTeam);
                int index = 0;
                teams[teamIndex].RacerPeople = new List<RacerPerson>();

                foreach (var name in firstNames)
                {
                    RacerPerson newRacerPerson = new RacerPerson
                    {
                        FirstName = name.FirstName,
                        LastName = lastNames[index].LastName,
                        Nation = lastNames[index].Nation,
                        Ability = _random.Next(10, 100),
                        Age = _random.Next(_minAge, _maxAge),
                        RacerPersonId = Guid.NewGuid()
                    };
                    newRacerPerson.PotentialAbility = _random.Next(newRacerPerson.Ability, 100);

                    if (teams[teamIndex] != null)
                    {
                        newRacerPerson.Team = teams[teamIndex];
                        teams[teamIndex].RacerPeople.Add(newRacerPerson);
                    }

                    index++;
                }

                teamIndex++;
            }

            return teamList;
        }
    }
}
