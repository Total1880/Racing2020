using Autofac;
using Racing.Repositories.RestClient;
using Racing.Services;
using Racing.ViewModel;
using static System.Configuration.ConfigurationManager;


namespace Racing
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

            builder.RegisterType<StartPageViewModel>().SingleInstance();
            builder.RegisterType<HomePageViewModel>().SingleInstance();
            builder.RegisterType<OverviewRacerPersonsViewModel>().SingleInstance();
            builder.RegisterType<RacePageViewModel>().SingleInstance();

            _container = builder.Build();
        }

        public StartPageViewModel StartPage => _container.Resolve<StartPageViewModel>();
        public HomePageViewModel HomePage => _container.Resolve<HomePageViewModel>();
        public OverviewRacerPersonsViewModel OverviewRacerPersons => _container.Resolve<OverviewRacerPersonsViewModel>();
        public RacePageViewModel RacePage => _container.Resolve<RacePageViewModel>();
    }
}
