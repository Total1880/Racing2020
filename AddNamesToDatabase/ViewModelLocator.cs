using AddNamesToDatabase.Repositories.RestClient;
using AddNamesToDatabase.Services;
using AddNamesToDatabase.ViewModel;
using Autofac;
using static System.Configuration.ConfigurationManager;

namespace AddNamesToDatabase
{
    public class ViewModelLocator
    {
        private readonly IContainer _container;

        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ServicesModule>();

            var _baseURI = AppSettings.Get("BasisURL");
            builder.RegisterType<RacingRestClient>().As<RacingRestClient>().WithParameter("baseAdress", _baseURI);

            builder.RegisterType<MainWindowViewModel>().SingleInstance();
            builder.RegisterType<NationPageViewModel>().SingleInstance();
            builder.RegisterType<RacePageViewModel>().SingleInstance();
            builder.RegisterType<SettingPageViewModel>().SingleInstance();
            builder.RegisterType<TeamPageViewModel>().SingleInstance();

            _container = builder.Build();
        }

        public MainWindowViewModel MainWindow => _container.Resolve<MainWindowViewModel>();
        public NationPageViewModel NationPage => _container.Resolve<NationPageViewModel>();
        public RacePageViewModel RacePage => _container.Resolve<RacePageViewModel>();
        public SettingPageViewModel SettingPage => _container.Resolve<SettingPageViewModel>();
        public TeamPageViewModel TeamPage => _container.Resolve<TeamPageViewModel>();
    }
}
