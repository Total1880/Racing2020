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

        public StartPage StartPage => _startPage ??= new StartPage();
        public HomePage HomePage => _homePage ??= new HomePage();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(StartPage);
            Messenger.Default.Register<OpenHomePageMessage>(this, OpenHomePage);
        }

        private void OpenHomePage(OpenHomePageMessage obj)
        {
            MainFrame.NavigationService.Navigate(HomePage);
        }
    }
}
