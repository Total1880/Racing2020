using GalaSoft.MvvmLight.Messaging;
using Racing.Messages.WindowOpener;
using System;
using System.Windows.Controls;

namespace Racing.Pages
{
    /// <summary>
    /// Interaction logic for SeasonOverviewPage.xaml
    /// </summary>
    public partial class SeasonOverviewPage : Page
    {
        private RaceResultPage _raceResultPage;
        private SeasonRankingPage _seasonRankingPage;
        private NextRaceInfoPage _nextRaceInfoPage;
        //private TeamOverviewPage _teamOverviewPage;

        public RaceResultPage RaceResultPage => _raceResultPage ??= new RaceResultPage();
        public SeasonRankingPage SeasonRankingPage => _seasonRankingPage ??= new SeasonRankingPage();
        public NextRaceInfoPage NextRaceInfoPage => _nextRaceInfoPage ??= new NextRaceInfoPage();
        //public TeamOverviewPage TeamOverviewPage => _teamOverviewPage ??= new TeamOverviewPage();

        public SeasonOverviewPage()
        {
            _raceResultPage = new RaceResultPage();
            _seasonRankingPage = new SeasonRankingPage();

            InitializeComponent();
            Messenger.Default.Register<OpenRaceResultPageMessage>(this, OpenRaceResultPage);
            Messenger.Default.Register<OpenSeasonRankingPageMessage>(this, OpenSeasonRankingPage);
            Messenger.Default.Register<OpenNextRaceInfoPageMessage>(this, OpenNextRaceInfoPage);
            Messenger.Default.Register<OpenTeamOverviewMessage>(this, OpenTeamOverviewPage);
        }

        private void OpenRaceResultPage(OpenRaceResultPageMessage obj)
        {
            SeasonOverviewFrame.Navigate(RaceResultPage);
        }

        private void OpenSeasonRankingPage(OpenSeasonRankingPageMessage obj)
        {
            SeasonOverviewFrame.Navigate(SeasonRankingPage);
        }

        private void OpenNextRaceInfoPage(OpenNextRaceInfoPageMessage obj)
        {
            SeasonOverviewFrame.Navigate(NextRaceInfoPage);
        }

        private void OpenTeamOverviewPage(OpenTeamOverviewMessage obj)
        {
            SeasonOverviewFrame.Navigate(new TeamOverviewPage());
        }
    }
}
