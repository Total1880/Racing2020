﻿using Autofac;

namespace AddNamesToDatabase.Repositories
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NationRepository>().AsImplementedInterfaces();
            builder.RegisterType<FirstNameRepository>().AsImplementedInterfaces();
            builder.RegisterType<LastNameRepository>().AsImplementedInterfaces();
            builder.RegisterType<RaceRepository>().AsImplementedInterfaces();
            builder.RegisterType<SettingRepository>().AsImplementedInterfaces();
            builder.RegisterType<TeamRepository>().AsImplementedInterfaces();
        }
    }
}
