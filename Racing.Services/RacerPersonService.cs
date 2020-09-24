using Racing.Repositories;
using Racing.Model;
using Racing.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Racing.Settings;

namespace Racing.Services
{
    public class RacerPersonService : IRacerPersonService
    {
        private INamesRepository<FirstNames> _firstNamesRepository;
        private INamesRepository<LastNames> _lastNamesRepository;
        private IRepository<Team> _teamRepository;
        private ISettingService _settingService;
        private readonly Random _random = new Random();
        private int _minAge = 16;
        private int _maxAge = 40;
        private int _minRacerStatsFacilityInfluence;

        public RacerPersonService(
            INamesRepository<FirstNames> firstNamesRepository,
            INamesRepository<LastNames> lastNamesRepository,
            IRepository<Team> teamRepository,
            ISettingService settingService)
        {
            _firstNamesRepository = firstNamesRepository;
            _lastNamesRepository = lastNamesRepository;
            _teamRepository = teamRepository;
            _settingService = settingService;

            _ = GetSettings();
        }

        private async Task GetSettings()
        {
            var setting = await _settingService.GetSettingByDescription(SettingsNames.MinRacerStatsFacilityInfluence);
            _minRacerStatsFacilityInfluence = int.Parse(setting.Value);
        }

        public async Task<IList<RacerPerson>> GenerateRacerPeople(int numberOfPeople, int numberOfTeams)
        {
            while (numberOfPeople % numberOfTeams != 0)
            {
                numberOfPeople--;
            }

            var firstNames = await _firstNamesRepository.GenerateNames(numberOfPeople);
            var lastNames = await _lastNamesRepository.GenerateNames(numberOfPeople);
            var teams = await _teamRepository.Get();
            var generatedRacerPeople = new List<RacerPerson>();
            int teamIndex = 0;
            int index = 0;

            foreach (var name in firstNames)
            {
                RacerPerson newRacerPerson = new RacerPerson
                {
                    FirstName = name.FirstName,
                    LastName = lastNames[index].LastName,
                    Nation = lastNames[index].Nation,
                    FlatAbility = _random.Next(10, 100),
                    ClimbingAbility = _random.Next(10, 100),
                    DownhillAbility = _random.Next(10, 100),
                    Age = _random.Next(_minAge, _maxAge)
                };
                newRacerPerson.FlatPotentialAbility = _random.Next(newRacerPerson.FlatAbility, 100);
                newRacerPerson.ClimbingPotentialAbility = _random.Next(newRacerPerson.ClimbingAbility, 100);
                newRacerPerson.DownhillPotentialAbility = _random.Next(newRacerPerson.DownhillAbility, 100);

                if (teams[teamIndex] != null)
                {
                    newRacerPerson.Team = teams[teamIndex];
                }

                generatedRacerPeople.Add(newRacerPerson);

                teamIndex++;
                index++;

                if (teamIndex >= numberOfTeams)
                {
                    teamIndex = 0;
                }
            }

            return generatedRacerPeople;
        }

        public IList<RacerPerson> GenerateUniqueId(IList<RacerPerson> racerPeople)
        {
            foreach (var racerPerson in racerPeople)
            {
                racerPerson.RacerPersonId = Guid.NewGuid();
            }

            return racerPeople;
        }

        public IList<RacerPerson> SeasonUpdateRacerPeople(IList<RacerPerson> racerPeople)
        {
            var updateRacerPeople = new List<RacerPerson>();

            foreach (var racerPerson in racerPeople)
            {
                if (racerPerson.Age < 20)
                {
                    racerPerson.FlatAbility += _random.Next(1, 10);
                    racerPerson.ClimbingAbility += _random.Next(1, 10);
                    racerPerson.DownhillAbility += _random.Next(1, 10);
                }
                else if (racerPerson.Age < 30)
                {
                    racerPerson.FlatAbility += _random.Next(1, 5);
                    racerPerson.ClimbingAbility += _random.Next(1, 5);
                    racerPerson.DownhillAbility += _random.Next(1, 5);
                }
                else if (racerPerson.Age < 40)
                {
                    racerPerson.FlatAbility -= _random.Next(1, 5);
                    racerPerson.ClimbingAbility -= _random.Next(1, 5);
                    racerPerson.DownhillAbility -= _random.Next(1, 5);
                }
                else
                {
                    var list = new List<RacerPerson>();
                    var task = Task.Run(async () => { list = (List<RacerPerson>)await GenerateRacerPeople(1, 1); });
                    task.Wait();

                    var newRacerPerson = list.SingleOrDefault();
                    newRacerPerson.Team = racerPerson.Team;
                    newRacerPerson.Age = _minAge;
                    newRacerPerson.FlatPotentialAbility += newRacerPerson.Team.YouthFacility + _minRacerStatsFacilityInfluence;
                    newRacerPerson.ClimbingPotentialAbility += newRacerPerson.Team.YouthFacility + _minRacerStatsFacilityInfluence;
                    newRacerPerson.DownhillPotentialAbility += newRacerPerson.Team.YouthFacility + _minRacerStatsFacilityInfluence;

                    if (newRacerPerson.FlatPotentialAbility > 99)
                        newRacerPerson.FlatPotentialAbility = 99;
                    if (newRacerPerson.ClimbingPotentialAbility > 99)
                        newRacerPerson.ClimbingPotentialAbility = 99;
                    if (newRacerPerson.DownhillPotentialAbility > 99)
                        newRacerPerson.DownhillPotentialAbility = 99;

                    if (newRacerPerson.FlatAbility > 50)
                        newRacerPerson.FlatAbility = 50;
                    if (newRacerPerson.ClimbingAbility > 50)
                        newRacerPerson.ClimbingAbility = 50;
                    if (newRacerPerson.DownhillAbility > 50)
                        newRacerPerson.DownhillAbility = 50;

                    newRacerPerson.RacerPersonId = Guid.NewGuid();
                    updateRacerPeople.Add(newRacerPerson);
                    continue;
                }

                racerPerson.FlatAbility += racerPerson.Team.TrainingFacility + _minRacerStatsFacilityInfluence;
                racerPerson.ClimbingAbility += racerPerson.Team.TrainingFacility + _minRacerStatsFacilityInfluence;
                racerPerson.DownhillAbility += racerPerson.Team.TrainingFacility + _minRacerStatsFacilityInfluence;

                if (racerPerson.FlatAbility > racerPerson.FlatPotentialAbility)
                    racerPerson.FlatAbility = racerPerson.FlatPotentialAbility;
                if (racerPerson.ClimbingAbility > racerPerson.ClimbingPotentialAbility)
                    racerPerson.ClimbingAbility = racerPerson.ClimbingPotentialAbility; 
                if (racerPerson.DownhillAbility > racerPerson.DownhillPotentialAbility)
                    racerPerson.DownhillAbility = racerPerson.DownhillPotentialAbility;

                if (racerPerson.FlatAbility < 10)
                    racerPerson.FlatAbility = 10;
                if (racerPerson.ClimbingAbility < 10)
                    racerPerson.ClimbingAbility = 10; 
                if (racerPerson.DownhillAbility < 10)
                    racerPerson.DownhillAbility = 10;

                racerPerson.Age++;
                racerPerson.Jersey = string.Empty;
                updateRacerPeople.Add(racerPerson);
            }

            return updateRacerPeople;
        }
    }
}
