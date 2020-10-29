using GalaSoft.MvvmLight.Messaging;
using Racing.Messages.WindowOpener;
using Racing.ViewModel;
using System;
using System.Windows.Controls;

namespace Racing.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private OverviewRacerPersons _overviewRacerPersons;
        private CurrentGameSettingsPage _currentGameSettingsPage;

        public OverviewRacerPersons OverviewRacerPersons => _overviewRacerPersons ??= new OverviewRacerPersons();
        public CurrentGameSettingsPage CurrentGameSettingsPage => _currentGameSettingsPage ??= new CurrentGameSettingsPage();

        public HomePage()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenOverviewRacerPersonsMessage>(this, OpenOverviewRacerPersons);
            Messenger.Default.Register<OpenCurrentGameSettingsMessage>(this, OpenCurrentGameSettings);
        }

        private void OpenOverviewRacerPersons(OpenOverviewRacerPersonsMessage obj)
        {
            HomePageFrame.NavigationService.Navigate(OverviewRacerPersons);            
        }

        private void OpenCurrentGameSettings(OpenCurrentGameSettingsMessage obj)
        {
            HomePageFrame.NavigationService.Navigate(CurrentGameSettingsPage);

        }
    }
}
