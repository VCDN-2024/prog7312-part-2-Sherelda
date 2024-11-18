using Microsoft.Win32;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace st10083869.prog7312.poe
{
    public partial class ReportIssuesWindow : Window
    {
        private Issue[] reportedIssues = new Issue[100]; // Array instead of List for better performance
        private int issueIndex = 0;
        private string attachmentPath;

        public ReportIssuesWindow()
        {
            InitializeComponent();
        }

        // Attach file handler
        private void btnAttach_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                attachmentPath = openFileDialog.FileName;
                txtAttachmentInfo.Text = $"Attached: {System.IO.Path.GetFileName(attachmentPath)}";
            }
        }

        // Submit button handler
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(txtLocation.Text) || cmbCategory.SelectedItem == null || string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Incomplete Form", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validate Location for address format (simple check for address format)
            if (!IsValidLocation(txtLocation.Text))
            {
                MessageBox.Show("The location address seems incomplete. Please provide a valid address or coordinates.", "Invalid Location", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Create new Issue
            Issue newIssue = new Issue
            {
                Location = txtLocation.Text,
                Category = ((ComboBoxItem)cmbCategory.SelectedItem).Content.ToString(),
                Description = txtDescription.Text,
                AttachmentPath = attachmentPath
            };

            // Store the issue
            reportedIssues[issueIndex++] = newIssue;

            // Simulate progress reporting (replace with async code in a real app)
            for (int i = 0; i <= 100; i += 10)
            {
                progressReporting.Value = i;
                System.Threading.Thread.Sleep(100); // Simulate work being done
            }

            MessageBox.Show("Issue reported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            ClearForm();
        }

        // Back to main menu
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        // Helper function to validate location
        private bool IsValidLocation(string location)
        {
            // Simple regex check for location format (can be improved)
            return Regex.IsMatch(location, @"^[a-zA-Z0-9\s,]+$");
        }

        // Clear form after submission
        private void ClearForm()
        {
            txtLocation.Clear();
            cmbCategory.SelectedIndex = -1;
            txtDescription.Clear();
            txtAttachmentInfo.Text = string.Empty;
            attachmentPath = null;
            progressReporting.Value = 0;
        }
    }

    public class Issue
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }
    }
}