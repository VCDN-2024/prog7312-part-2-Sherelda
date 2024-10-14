using Microsoft.Win32;
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
   
    public partial class ReportIssuesWindow : Window
 
        {
         private List<Issue> reportedIssues = new List<Issue>();
        private string attachmentPath;

        public ReportIssuesWindow()
        {

            InitializeComponent();
        }

        //Used to attch image 
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

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLocation.Text) || cmbCategory.SelectedItem == null || string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Incomplete Form", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Issue newIssue = new Issue
            {
                Location = txtLocation.Text,
                Category = ((ComboBoxItem)cmbCategory.SelectedItem).Content.ToString(),
                Description = txtDescription.Text,
                AttachmentPath = attachmentPath
            };

            reportedIssues.Add(newIssue);

            // Simulating progress
            for (int i = 0; i <= 100; i += 10)
            {
                progressReporting.Value = i;
                System.Threading.Thread.Sleep(100); // This is for demonstration. In a real app, use async/await instead.
            }

            MessageBox.Show("Issue reported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            ClearForm();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

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
