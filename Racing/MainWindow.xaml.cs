using GalaSoft.MvvmLight.Messaging;
using Racing.Messages.WindowOpener;
using Racing.Pages;
using System;
using System.Windows;

namespace Racing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StartPage _startPage;
        private HomePage _homePage;
        private SeasonOverviewPage _seasonOverviewPage;

        public StartPage StartPage => _startPage ??= new StartPage();
        public HomePage HomePage => _homePage ??= new HomePage();
        public RacePage RacePage => new RacePage();
        public SeasonOverviewPage SeasonOverviewPage => _seasonOverviewPage ??= new SeasonOverviewPage();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(StartPage);
            Messenger.Default.Register<OpenHomePageMessage>(this, OpenHomePage);
            Messenger.Default.Register<OpenSeasonOverviewPageMessage>(this, OpenSeasonOverviewPage);
            Messenger.Default.Register<OpenRacePageMessage>(this, OpenRacePage);
        }

        private void OpenHomePage(OpenHomePageMessage obj)
        {
            MainFrame.NavigationService.Navigate(HomePage);
        }

        private void OpenSeasonOverviewPage(OpenSeasonOverviewPageMessage obj)
        {
            MainFrame.NavigationService.Navigate(SeasonOverviewPage);
        }

        private void OpenRacePage(OpenRacePageMessage obj)
        {
            MainFrame.NavigationService.Navigate(RacePage);
        }
    }
}
