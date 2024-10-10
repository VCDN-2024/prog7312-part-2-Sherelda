using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace st10083869.prog7312.poe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //linking of buttons 
        private void btnLocalEvents_Click(object sender, RoutedEventArgs e)
        {
            LocalEventsWindow localEventsWindow = new LocalEventsWindow();
            localEventsWindow.Show();
            this.Hide();
        }

        private void btnReportIssues_Click(object sender, RoutedEventArgs e)
        {
           ReportIssuesWindow reportIssuesWindow = new ReportIssuesWindow();
            reportIssuesWindow.Show();
            this.Hide();
        }

        private void btnServiceRequestStatus_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Service Request Status clicked");
        }

        private void btnProvideFeedback_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Provide Feedback clicked");
        }
    }
}
      