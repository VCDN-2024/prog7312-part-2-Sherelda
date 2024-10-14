# Community Engagement Application

## Overview

This Windows Presentation Foundation (WPF) application is developed to enhance community engagement and manage local events effectively. It provides a user-friendly platform for users to interact with local events, report issues, and share feedback with community organizers.

## Key Features

1. **Main Menu**
   - Acts as the central hub for accessing various functions within the app.
   - Features a visually appealing design with a gradient background.

2. **Local Events and Announcements**
   - Allows users to view, add, edit, and delete local events.
   - Includes search and filter options by category and date.
   - Provides personalized event recommendations.
   - Utilizes advanced data structures for efficient event management:
     - `SortedDictionary` to organize events by date.
     - `HashSet` for unique categories.
     - `Stack` for managing search history.
     - `Queue` for upcoming events.
     - `PriorityQueue` for events with prioritized importance.

3. **Report Issues**
   - Users can report local issues by providing details such as location, category, and description.
   - Allows users to attach image files to provide additional context.
   - A progress bar shows the status of submissions.

4. **Submit Feedback**
   - Users can send feedback, comments, and suggestions.
   - Feedback is stored for future analysis.

5. **Service Request Status** (Future Feature)
   - Placeholder for a future feature to track service request statuses.

## Installation Instructions

1. Install Visual Studio with .NET Framework support for WPF applications.
2. Clone or download the project repository https://github.com/VCDN-2024/prog7312-part-2-Sherelda/tree/master 
3. Open the solution file using Visual Studio.
4. Build the solution to restore NuGet packages and compile the project.
5. Run the application using F5 or the "Start" button in Visual Studio.

## User Guide

### Main Menu

- On launching the app, the main menu provides navigation options:
  - Local Events and Announcements
  - Report Issues
  - Service Request Status (feature under development)
  - Provide Feedback

### Local Events and Announcements

![Screenshot (155)](https://github.com/user-attachments/assets/e6c54484-c1b6-4e98-a3a3-5d3072f696fc)
- Browse through a list of local events.
- Use the search feature to filter events by category or date.
- Add new events or edit and delete existing ones.
- View personalized event recommendations.

### Report Issues

![Screenshot (156)](https://github.com/user-attachments/assets/d4d7eae2-e1cb-444f-956d-fc2a667127df)
- Complete the form with the issue's location, category, and description.
- Optionally, attach an image file to support your report.
- Submit the issue by clicking the "Submit" button.
![Screenshot (157)](https://github.com/user-attachments/assets/b4e01868-d6d9-45bb-b8f1-fccb7594f14d)


### Provide Feedback

![Screenshot (159)](https://github.com/user-attachments/assets/69cb1498-92f3-4e69-8dd2-aef110b5a8ec)
- Fill in your details along with your comments and suggestions.
- Use "Send" to submit the feedback or "Clear" to reset the form.

## Project Files

- `MainWindow.xaml` & `MainWindow.xaml.cs`: Main window and navigation.
- `LocalEventsWindow.xaml` & `LocalEventsWindow.xaml.cs`: Local events management interface.
- `ReportIssuesWindow.xaml` & `ReportIssuesWindow.xaml.cs`: Interface for reporting issues.
- `ProvideFeedbackWindow.xaml` & `ProvideFeedbackWindow.xaml.cs`: Feedback submission interface.
- `AddEventWindow.xaml` & `AddEventWindow.xaml.cs`: Interface for adding/editing events.
- `Event.cs`: Class definition for event objects.

## Technical Details

- User interface is created with XAML, and backend logic is in C#.
- The project demonstrates the use of C# data structures and LINQ for optimized data handling.
- UI design follows a consistent color theme with gradient backgrounds.

## Author

ST10083869

---

For any questions, suggestions, or contributions, contact the project maintainer or submit an issue via the project repository.
