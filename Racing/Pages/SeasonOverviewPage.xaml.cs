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

        public RaceResultPage RaceResultPage => _raceResultPage ??= new RaceResultPage();

        public SeasonOverviewPage()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenRaceResultPageMessage>(this, OpenRaceResultPage);
        }

        private void OpenRaceResultPage(OpenRaceResultPageMessage obj)
        {
            SeasonOverviewFrame.Navigate(RaceResultPage);
        }
    }
}
