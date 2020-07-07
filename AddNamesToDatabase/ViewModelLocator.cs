using AddNamesToDatabase.ViewModel;
using Autofac;

namespace AddNamesToDatabase
{
    public class ViewModelLocator
    {
        private readonly IContainer _container;

        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindowViewModel>().SingleInstance();

            _container = builder.Build();
        }

        public MainWindowViewModel MainWindow => _container.Resolve<MainWindowViewModel>();
    }
}
