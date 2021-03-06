﻿using Autofac;
using Racing.Repositories;

namespace Racing.Services
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<RepositoriesModule>();

            builder.RegisterType<RacerPersonService>().AsImplementedInterfaces();
            builder.RegisterType<RaceEngineServiceV2>().AsImplementedInterfaces();
            builder.RegisterType<SeasonEngineService>().AsImplementedInterfaces();
            builder.RegisterType<RaceService>().AsImplementedInterfaces();
            builder.RegisterType<SettingService>().AsImplementedInterfaces();
            builder.RegisterType<DivisionService>().AsImplementedInterfaces();
            builder.RegisterType<TeamService>().AsImplementedInterfaces();
            builder.RegisterType<SaveGameDivisionService>().AsImplementedInterfaces();
            builder.RegisterType<FacilityUpgradeEngine>().AsImplementedInterfaces();
            builder.RegisterType<SaveGameSettingsService>().AsImplementedInterfaces();
        }
    }
}
