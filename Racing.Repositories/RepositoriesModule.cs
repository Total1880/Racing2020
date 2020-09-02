using Autofac;

namespace Racing.Repositories
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FirstNamesRepository>().AsImplementedInterfaces();
            builder.RegisterType<LastNamesRepository>().AsImplementedInterfaces();
            builder.RegisterType<RaceRepository>().AsImplementedInterfaces();
            builder.RegisterType<SettingRepository>().AsImplementedInterfaces();
            builder.RegisterType<TeamRepository>().AsImplementedInterfaces();
            builder.RegisterType<XmlSaveGameDivisionRepository>().AsImplementedInterfaces();
        }
    }
}
