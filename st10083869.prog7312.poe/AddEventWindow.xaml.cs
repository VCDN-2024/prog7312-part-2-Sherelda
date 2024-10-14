using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    //Author st10083869
    //prog7312 part2 

    public partial class AddEventWindow : Window
    {
        //Gets and sets used to gather and store data 
        public Event NewEvent { get; private set; }
        private Event EventToEdit { get; set; }

        public AddEventWindow()
        {
            InitializeComponent();
            EventDatePicker.SelectedDate = DateTime.Now;
            this.Closing += AddEventWindow_Closing;
        }// closing the window 

        //used to add the event to the program 
        //system gathers the details of the description,date,title..
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) ||
                !EventDatePicker.SelectedDate.HasValue ||
                string.IsNullOrWhiteSpace(CategoryTextBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                //message box if a field is not filled in 
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewEvent = new Event(
                TitleTextBox.Text,
                EventDatePicker.SelectedDate.Value,
                CategoryTextBox.Text,
                DescriptionTextBox.Text
            );

            DialogResult = true;
            Close();
        }

        public AddEventWindow(Event eventToEdit) : this()
        {
            EventToEdit = eventToEdit;
            PopulateFields();
            this.Title = "Edit Event";
        }

        private void PopulateFields()
        {
            if (EventToEdit != null)
            {
                TitleTextBox.Text = EventToEdit.Title;
                EventDatePicker.SelectedDate = EventToEdit.Date;
                CategoryTextBox.Text = EventToEdit.Category;
                DescriptionTextBox.Text = EventToEdit.Description;
            }
        }


        //cancel button used to cancel the add event .it will go back to the previous window
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void AddEventWindow_Closing(object sender, CancelEventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
            }
        }
    }
}