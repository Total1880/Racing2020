using AddNamesToDatabase.Repositories;
using Autofac;

namespace AddNamesToDatabase.Services
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<RepositoriesModule>();

            builder.RegisterType<NationService>().AsImplementedInterfaces();
        }
    }
}
