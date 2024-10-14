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
    {   //Data structures are implemented to manage the events by history,dates,category 
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
        private const string EventsFilePath = "events.xml";

        public LocalEventsWindow()
        {
            InitializeComponent();
            InitializeDataStructures();
            LoadEvents();
            UpdateUI();
        }

        //data structures 

        //code attribution :
        //Codecamp
        //www.youtube.com
        //Link :https://youtu.be/RBSGKlAvoiM?si=tDMhwiEd8kGfdXmd
        private void InitializeDataStructures()
        {
            eventsByDate = new SortedDictionary<DateTime, List<Event>>();
            categories = new HashSet<string>();
            searchHistory = new Stack<string>();
            upcomingEvents = new Queue<Event>();
            priorityEvents = new PriorityQueue<Event, DateTime>();
        }

        private void LoadEvents()
        {
            try
            {
                if (File.Exists(EventsFilePath))
                {
                    using (var reader = new StreamReader(EventsFilePath))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<Event>));
                        var events = (List<Event>)serializer.Deserialize(reader);
                        foreach (var evt in events)
                        {
                            AddEvent(evt);//Adds each event to the dat struct
                        }
                    }
                }
                else
                {
                    PopulateInitialData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading events: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                PopulateInitialData();//uses the initial infor to populate
            }
        }

        private void SaveEvents()
        {
            try
            {
                var allEvents = eventsByDate.SelectMany(kvp => kvp.Value).ToList();
                using (var writer = new StreamWriter(EventsFilePath)) // Flatten the dictionary to a list of events
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Event>));
                    serializer.Serialize(writer, allEvents);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving events: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PopulateInitialData()
        {
            //Data for examples 
            AddEvent(new Event("Community movie Night", DateTime.Now.AddDays(5), "Community", "Join us for a movie night,eveyone is welcomed!"));
            AddEvent(new Event("Local Art expo", DateTime.Now.AddDays(10), "Art", "Join us as local artist are showing casing their art ."));
            AddEvent(new Event("Charity Run", DateTime.Now.AddDays(15), "Sports", "5K run to raise funds for local charities."));
            AddEvent(new Event("Town meeting Meeting", DateTime.Now.AddDays(7), "Civic", "Discussing upcoming events or any matters."));
        }

        private void AddEvent(Event newEvent)
        {
            if (!eventsByDate.ContainsKey(newEvent.Date.Date))
            {
                eventsByDate[newEvent.Date.Date] = new List<Event>();// initalizes the dates if it's not present
            }
            eventsByDate[newEvent.Date.Date].Add(newEvent);//Allows for the adding of events to the specific dates 
            categories.Add(newEvent.Category);//Adds the category to the specific set of the category 
            upcomingEvents.Enqueue(newEvent);//Adds new events queue
            priorityEvents.Enqueue(newEvent, newEvent.Date);//Adds to the queue depending on its date 
        }

        private void UpdateUI()
        {
            CategoryComboBox.ItemsSource = categories;//sets the categories got the combo
            EventsListView.ItemsSource = eventsByDate.SelectMany(kvp => kvp.Value).OrderBy(e => e.Date).ToList();//Displays the events 
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {//selects the cat and date 
                var selectedCategory = CategoryComboBox.SelectedItem as string;
                var selectedDate = EventDatePicker.SelectedDate;

                var filteredEvents = eventsByDate
                    .Where(kvp => (!selectedDate.HasValue || kvp.Key == selectedDate.Value.Date) &&
                                  (string.IsNullOrEmpty(selectedCategory) || kvp.Value.Any(e => e.Category == selectedCategory)))
                    .SelectMany(kvp => kvp.Value)
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
                AddEvent(addEventWindow.NewEvent);
                SaveEvents();
                UpdateUI();
            }

            //opens a new window
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
            categories.Remove(eventToRemove.Category);
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
                    AddEvent(editEventWindow.NewEvent);
                    SaveEvents();
                    UpdateUI();
                }

                this.Show();
            }
        }

        //Delte button
        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedEvent = ((Button)sender).DataContext as Event;
            if (selectedEvent != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the event '{selectedEvent.Title}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    RemoveEvent(selectedEvent);
                    SaveEvents();
                    UpdateUI();
                }
            }
        }



        private void BackToMainButton_Click(object sender, RoutedEventArgs e)
        {
            SaveEvents();
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