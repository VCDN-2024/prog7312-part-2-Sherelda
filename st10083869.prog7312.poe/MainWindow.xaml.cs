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
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //linking of buttons 
        //part2
        private void btnLocalEvents_Click(object sender, RoutedEventArgs e)
        {
            LocalEventsWindow localEventsWindow = new LocalEventsWindow();
            localEventsWindow.Show();
            this.Hide();
        }

        //part 2
        private void btnReportIssues_Click(object sender, RoutedEventArgs e)
        {
           ReportIssuesWindow reportIssuesWindow = new ReportIssuesWindow();
            reportIssuesWindow.Show();
            this.Hide();
        }

        //part 3
        private void btnServiceRequestStatus_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Service Request Status clicked");
        }

        //part 1
        private void btnProvideFeedback_Click(object sender, RoutedEventArgs e)
        {
            ProvideFeedbackWindow provideFeedbackWindow = new ProvideFeedbackWindow();
            provideFeedbackWindow.Show();
            this.Hide();
        }
        private void btnBackToMain_Click(object sender, RoutedEventArgs e)
{
    // Logic to return to main menu
    MainWindow mainWindow = new MainWindow();
    mainWindow.Show();
    this.Close();
}
    }
}
      