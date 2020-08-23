using AddNamesToDatabase.Repositories;
using AddNamesToDatabase.Services.Interfaces;
using Autofac;

namespace AddNamesToDatabase.Services
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<RepositoriesModule>();

            builder.RegisterType<NationService>().AsImplementedInterfaces();
            builder.RegisterType<NameService>().AsImplementedInterfaces();
            builder.RegisterType<RaceService>().AsImplementedInterfaces();
            builder.RegisterType<SettingService>().AsImplementedInterfaces();
            builder.RegisterType<TeamService>().AsImplementedInterfaces();
        }
    }
}
