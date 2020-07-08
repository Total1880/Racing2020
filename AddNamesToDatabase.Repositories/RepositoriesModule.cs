using Autofac;

namespace AddNamesToDatabase.Repositories
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NationRepository>().AsImplementedInterfaces();
        }
    }
}
