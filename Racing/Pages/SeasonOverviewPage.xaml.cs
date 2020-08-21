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

        public RaceResultPage RaceResultPage => _raceResultPage ??= new RaceResultPage();
        public SeasonRankingPage SeasonRankingPage => _seasonRankingPage ??= new SeasonRankingPage();

        public SeasonOverviewPage()
        {
            _raceResultPage = new RaceResultPage();
            _seasonRankingPage = new SeasonRankingPage();

            InitializeComponent();
            Messenger.Default.Register<OpenRaceResultPageMessage>(this, OpenRaceResultPage);
            Messenger.Default.Register<OpenSeasonRankingPageMessage>(this, OpenSeasonRankingPage);
        }

        private void OpenRaceResultPage(OpenRaceResultPageMessage obj)
        {
            SeasonOverviewFrame.Navigate(RaceResultPage);
        }

        private void OpenSeasonRankingPage(OpenSeasonRankingPageMessage obj)
        {
            SeasonOverviewFrame.Navigate(SeasonRankingPage);
        }
    }
}
