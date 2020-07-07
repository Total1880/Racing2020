using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AddNamesToDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainPage _mainPage;
        private NationPage _nationPage;

        private MainPage MainPage => _mainPage ??= new MainPage();
        private NationPage NationPage => _nationPage ??= new NationPage();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(MainPage);
        }

        private void Button_Click_New_List(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(MainPage);
        }

        private void Button_Click_New_Nation(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(NationPage);
        }
    }
}
