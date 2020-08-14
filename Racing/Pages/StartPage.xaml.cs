using System.Windows.Controls;

namespace Racing.Pages
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var ANTD = new AddNamesToDatabase.MainWindow();
            ANTD.Show();
        }
    }
}
