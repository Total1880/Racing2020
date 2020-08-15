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

        public OverviewRacerPersons OverviewRacerPersons => _overviewRacerPersons ??= new OverviewRacerPersons();

        public HomePage()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenOverviewRacerPersonsMessage>(this, OpenOverviewRacerPersons);
        }

        private void OpenOverviewRacerPersons(OpenOverviewRacerPersonsMessage obj)
        {
            HomePageFrame.NavigationService.Navigate(OverviewRacerPersons);            
        }
    }
}
