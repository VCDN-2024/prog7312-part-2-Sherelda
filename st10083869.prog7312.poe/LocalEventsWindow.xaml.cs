using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;

namespace st10083869.prog7312.poe
{
    //Author :st10083869
    //prog7312 part2 


    public partial class LocalEventsWindow : Window
    {
        //Data structures are implemented to manage the events by history,dates,category 
        //stores the evnts by the dates 
        private SortedDictionary<DateTime, List<Event>> eventsByDate;
        //Ensures that the events are stored by category
        private HashSet<string> categories;
        //Allows for storing the history of catory searches 
        private Stack<string> searchHistory;
        //Ques are used for tracking the upcoming events 
        private Queue<Event> upcomingEvents;
        //Events are priorotized by the dates 
        private PriorityQueue<Event, DateTime> priorityEvents;

        public LocalEventsWindow()
        {
            InitializeComponent();
            InitializeDataStructures();
            UpdateUI();
        }
        //data structures 

        //code attribution :
        //Codecamp
        //www.youtube.com
        //Link :https://youtu.be/RBSGKlAvoiM?si=tDMhwiEd8kGfdXmd
        private void InitializeDataStructures()
        {
            // Hard-code SortedDictionary for events by date
            eventsByDate = new SortedDictionary<DateTime, List<Event>>
            {
                { DateTime.Now.AddDays(5).Date, new List<Event> { new Event("Community Movie Night", DateTime.Now.AddDays(5), "Community", "Join us for a movie night, everyone is welcomed!") } },
                { DateTime.Now.AddDays(10).Date, new List<Event> { new Event("Local Art Expo", DateTime.Now.AddDays(10), "Art", "Join us as local artists are showcasing their art.") } },
                { DateTime.Now.AddDays(15).Date, new List<Event> { new Event("Charity Run", DateTime.Now.AddDays(15), "Sports", "R10000 run to raise funds for local charities.") } },
                { DateTime.Now.AddDays(7).Date, new List<Event> { new Event("Halloween party ", DateTime.Now.AddDays(7), "Charity", "everyone has to dress as a character.") } }
            };

            // Hard-code HashSet for categories
            categories = new HashSet<string> { "Community", "Art", "Sports", "Charity" };

            // Hard-code Stack for search history
            searchHistory = new Stack<string>(new[] { "Art", "Community","Sports","charity" });

            // Hard-code Queue for upcoming events
            upcomingEvents = new Queue<Event>(eventsByDate.Values.SelectMany(list => list).OrderBy(e => e.Date));

            // Hard-code PriorityQueue for priority events
            priorityEvents = new PriorityQueue<Event, DateTime>();
            foreach (var evt in eventsByDate.Values.SelectMany(list => list))
            {
                priorityEvents.Enqueue(evt, evt.Date);
            }
        }

        private void UpdateUI()
        {
            CategoryComboBox.ItemsSource = categories.ToList();//sets the categories for the combo
            EventsListView.ItemsSource = eventsByDate.SelectMany(kvp => kvp.Value).OrderBy(e => e.Date).ToList();//Displays the events 
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {//selects the categories  and date 
                var selectedCategory = CategoryComboBox.SelectedItem as string;
                var selectedDate = EventDatePicker.SelectedDate;

                var filteredEvents = eventsByDate
                    .Where(kvp => (!selectedDate.HasValue || kvp.Key == selectedDate.Value.Date))
                    .SelectMany(kvp => kvp.Value)
                    .Where(evt => string.IsNullOrEmpty(selectedCategory) || evt.Category == selectedCategory)
                    .OrderBy(e => e.Date)
                    .ToList();

                EventsListView.ItemsSource = filteredEvents;//Displaying the filter 

                if (!string.IsNullOrEmpty(selectedCategory))
                {
                    searchHistory.Push(selectedCategory);//Allows for the adding of the search history 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during search: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Code attribution 
        //Author:Shiyu Wang 
        //www.youtube.com
        //Link:https://youtu.be/kJFpLy7trKs?si=lmAWXMUBOH7hVj9e
        private void ViewRecommendationsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var recommendations = GetRecommendations(); // allows for the data to be fetched 
                MessageBox.Show(string.Join("\n", recommendations), "Recommended Events", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting recommendations: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //generates a list of recommended events depending on the category 
        private List<string> GetRecommendations()
        {
            var recommendations = new List<string>();

            //provides suggests depending on the search catorgy 
            if (searchHistory.Count > 0)
            {
                var topCategory = searchHistory.Peek();
                var relatedEvents = eventsByDate
                    .SelectMany(kvp => kvp.Value)
                    .Where(e => e.Category == topCategory && e.Date > DateTime.Now)
                    .Take(2);
                recommendations.AddRange(relatedEvents.Select(e => $"Based on your searches: {e.Title} ({e.Date.ToShortDateString()})"));
            }

            var nextUpcomingEvent = upcomingEvents.FirstOrDefault(e => e.Date > DateTime.Now);
            if (nextUpcomingEvent != null)
            {
                recommendations.Add($"Next upcoming event: {nextUpcomingEvent.Title} ({nextUpcomingEvent.Date.ToShortDateString()})");
            }

            while (recommendations.Count < 5 && priorityEvents.TryDequeue(out Event priorityEvent, out _))
            {
                if (priorityEvent.Date > DateTime.Now)
                {
                    recommendations.Add($"Featured event: {priorityEvent.Title} ({priorityEvent.Date.ToShortDateString()})");
                }
            }

            return recommendations;
        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEventWindow();
            addEventWindow.Owner = this;
            this.Hide();

            if (addEventWindow.ShowDialog() == true)
            {
                AddNewEvent(addEventWindow.NewEvent); 
                UpdateUI();
            }

            this.Show();
        }

        //Deleting of the event 
        private void RemoveEvent(Event eventToRemove)
        {
            if (eventsByDate.ContainsKey(eventToRemove.Date.Date))
            {
                var dateEvents = eventsByDate[eventToRemove.Date.Date];
                dateEvents.Remove(eventToRemove);
                if (dateEvents.Count == 0)
                {
                    eventsByDate.Remove(eventToRemove.Date.Date);
                }
            }
            //The queue managers the events so it is calling it to be able to delete 
            if (!eventsByDate.Values.SelectMany(list => list).Any(e => e.Category == eventToRemove.Category))
            {
                categories.Remove(eventToRemove.Category);
            }
            upcomingEvents = new Queue<Event>(upcomingEvents.Where(e => e != eventToRemove));
            priorityEvents = new PriorityQueue<Event, DateTime>(
                priorityEvents.UnorderedItems.Where(item => item.Element != eventToRemove)
                    .Select(item => (item.Element, item.Priority)));
        }

        // Allows the events to be edited 
        private void EditEventButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedEvent = ((Button)sender).DataContext as Event;
            if (selectedEvent != null)
            {
                var editEventWindow = new AddEventWindow(selectedEvent);
                editEventWindow.Owner = this;
                this.Hide();

                if (editEventWindow.ShowDialog() == true)
                {
                    RemoveEvent(selectedEvent);
                    AddNewEvent(editEventWindow.NewEvent); 
                    UpdateUI();
                }

                this.Show();
            }
        }

        private void AddNewEvent(Event newEvent)
        {
            if (!eventsByDate.TryGetValue(newEvent.Date.Date, out var eventsOnDate))
            {
                eventsOnDate = new List<Event>();
                eventsByDate[newEvent.Date.Date] = eventsOnDate;
            }
            eventsOnDate.Add(newEvent);
            categories.Add(newEvent.Category);
            upcomingEvents.Enqueue(newEvent);
            priorityEvents.Enqueue(newEvent, newEvent.Date);
        }

        //Delete button
        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedEvent = ((Button)sender).DataContext as Event;
            if (selectedEvent != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the event '{selectedEvent.Title}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    RemoveEvent(selectedEvent);
                    UpdateUI();
                }
            }
        }

        private void BackToMainButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }

    public class Event
    {
        //gets and sets used for gathering and storing information 
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public Event() { }

        public Event(string title, DateTime date, string category, string description)
        {
            Title = title;
            Date = date;
            Category = category;
            Description = description;
        }
    }
}