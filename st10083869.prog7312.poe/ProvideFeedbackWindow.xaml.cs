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
using System.Windows.Shapes;

namespace st10083869.prog7312.poe
{
    
    public partial class ProvideFeedbackWindow : Window
    {
        private List<Feedback> feedbackList = new List<Feedback>();

        public ProvideFeedbackWindow()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve data from the form
            string name = txtName.Text;
            string email = txtEmailID.Text;
            string comments = txtComments.Text;
            string suggestions = txtSuggestions.Text;

            // Validate inputs
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(comments))
            {
                MessageBox.Show("Please fill in all the required fields.");
                return;
            }

            // Store data in the list
            feedbackList.Add(new Feedback
            {
                Name = name,
                Email = email,
                Comments = comments,
                Suggestions = suggestions
            });

            // Clear fields after sending
            txtName.Clear();
            txtEmailID.Clear();
            txtComments.Clear();
            txtSuggestions.Clear();

            MessageBox.Show("Feedback sent successfully.");
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // Clear all fields
            txtName.Clear();
            txtEmailID.Clear();
            txtComments.Clear();
            txtSuggestions.Clear();
        }
        private void btnBackToMain_Click(object sender, RoutedEventArgs e)
        {
            // Logic to return to main menu
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private class Feedback
        {
            // GETS AND SETS USED TO GATHER AND STORE DATA
            public string Name { get; set; }
            public string Email { get; set; }
            public string Comments { get; set; }
            public string Suggestions { get; set; }
        }
    }
}