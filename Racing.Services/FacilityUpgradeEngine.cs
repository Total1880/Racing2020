using Racing.Model;
using Racing.Services.Interfaces;
using Racing.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.Services
{
    class FacilityUpgradeEngine : IFacilityUpgradeEngine
    {
        private readonly ISettingService _settingService;
        private int _factorFacilityUpgrade;
        private int _basicPriceFacilityUpgrade;
        private int _yearsWhenFacilityDowngrade;
        private readonly Random _random = new Random();

        public FacilityUpgradeEngine(ISettingService settingService)
        {
            _settingService = settingService;
            _ = GetSettings();
        }
        public IList<Division> Upgrade(IList<Division> divisions)
        {
            foreach (var division in divisions)
            {
                foreach (var team in division.TeamList)
                {
                    var costTrainingFacilityUpgrade = ((team.TrainingFacility + 1) * _basicPriceFacilityUpgrade) * _factorFacilityUpgrade;
                    var costYouthFacilityUpgrade = ((team.YouthFacility + 1) * _basicPriceFacilityUpgrade) * _factorFacilityUpgrade;

                    if (costTrainingFacilityUpgrade > team.Budget && costYouthFacilityUpgrade > team.Budget)
                    {
                        continue;
                    }

                    if (team.FacilityUpgradePreference == FacilityUpgradePreference.Training)
                    {
                        if ((team.TrainingFacility - team.YouthFacility) == 3 || costTrainingFacilityUpgrade > team.Budget)
                        {
                            team.YouthFacility++;
                            team.Budget -= costYouthFacilityUpgrade;
                        }
                        else
                        {
                            team.TrainingFacility++;
                            team.Budget -= costTrainingFacilityUpgrade;
                        }
                    }
                    else if (team.FacilityUpgradePreference == FacilityUpgradePreference.Youth)
                    {
                        if ((team.YouthFacility - team.TrainingFacility) == 3 || costYouthFacilityUpgrade > team.Budget)
                        {
                            team.TrainingFacility++;
                            team.Budget -= costTrainingFacilityUpgrade;
                        }
                        else
                        {
                            team.YouthFacility++;
                            team.Budget -= costYouthFacilityUpgrade;
                        }

                        Downgrade(team);
                    }
                }
            }

            return divisions;
        }
        private void Downgrade(Team team)
        {
            if (_random.Next(1, _yearsWhenFacilityDowngrade) == _yearsWhenFacilityDowngrade - 1 && team.TrainingFacility > 0)
            {
                team.TrainingFacility--;
            }

            if (_random.Next(1, _yearsWhenFacilityDowngrade) == _yearsWhenFacilityDowngrade - 1 && team.YouthFacility > 0)
            {
                team.YouthFacility--;
            }
        }

        private async Task GetSettings()
        {
            var factorFacilityUpgradeSetting = await _settingService.GetSettingByDescription(SettingsNames.FactorFacilityUpgrade);
            var basicPriceFacilityUpgradeSetting = await _settingService.GetSettingByDescription(SettingsNames.BasicPriceFacilityUpgrade);
            var yearsWhenFacilityDowngradeSetting = await _settingService.GetSettingByDescription(SettingsNames.YearsWhenFacilityDowngrade);

            _factorFacilityUpgrade = int.Parse(factorFacilityUpgradeSetting.Value);
            _basicPriceFacilityUpgrade = int.Parse(basicPriceFacilityUpgradeSetting.Value);
            _yearsWhenFacilityDowngrade = int.Parse(yearsWhenFacilityDowngradeSetting.Value);
        }
    }
}
