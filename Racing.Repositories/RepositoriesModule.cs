using Autofac;

namespace Racing.Repositories
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FirstNamesRepository>().AsImplementedInterfaces();
            builder.RegisterType<LastNamesRepository>().AsImplementedInterfaces();
        }
    }
}
