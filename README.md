# Community Engagement Application

## Overview

This Windows Presentation Foundation (WPF) application is developed to enhance community engagement and manage local events effectively. It provides a user-friendly platform for users to interact with local events, report issues, and share feedback with community organizers.

**Demo video link** https://youtu.be/Yo3eSZb_Y9E?feature=shared 
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

5. **Service Request Status**
- Advanced request tracking and management system
- Implements multiple data structures for efficient handling:
**1** Red-Black Tree for balanced search operations
**2** Min Heap for priority-based request handling
**3** Graph implementation for service area management
- Features sophisticated search and sort capabilities
- Visual representation of service request statistics

## Installation Instructions

1. Install Visual Studio with .NET Framework support for WPF applications.
2. Clone or download the project repository https://github.com/VCDN-2024/prog7312-part-2-Sherelda/tree/master 
3. Open the solution file using Visual Studio.
4. Build the solution to restore NuGet packages and compile the project.
5. Run the application using F5 or the "Start" button in Visual Studio.

## User Guide

### Main Menu
![Screenshot (155)](https://github.com/user-attachments/assets/e6c54484-c1b6-4e98-a3a3-5d3072f696fc)
- On launching the app, the main menu provides navigation options:
  - Local Events and Announcements
  - Report Issues
  - Service Request Status (feature under development)
  - Provide Feedback

### Local Events and Announcements

![Screenshot (156)](https://github.com/user-attachments/assets/d4d7eae2-e1cb-444f-956d-fc2a667127df)
- Browse through a list of local events.
- Use the search feature to filter events by category or date.
- Add new events or edit and delete existing ones.
- View personalized event recommendations.

### Report Issues

![Screenshot (22)](https://github.com/user-attachments/assets/c695eda3-0b28-43c8-9222-d659ab2db10e)
- Complete the form with the issue's location, category, and description.
- Optionally, attach an image file to support your report.
- Submit the issue by clicking the "Submit" button.

### Provide Feedback

![Screenshot (159)](https://github.com/user-attachments/assets/69cb1498-92f3-4e69-8dd2-aef110b5a8ec)
- Fill in your details along with your comments and suggestions.
- Use "Send" to submit the feedback or "Clear" to reset the form.

  ## Service Request Status 
![Screenshot (160)](https://github.com/user-attachments/assets/2b09b899-d1fc-41fe-a2d3-506d8de5f840)
-Users will be able to search according to the users ID,descrption,Priortyand area and the graph will show the status of request.

  ## Data Structures:
1. **Graph Implementation (Graph.cs)**
- Used to manage the service areas and their relationships. Allows for the representation of service areas, modeling of relationships, traversal algorithms, and computation of the Minimum Spanning Tree (MST).

2. **AVL trees**
- Used for storing and retriving requests

3. **Min Heap (MinHeap.cs)**
- Used for priority-based handling of service requests. Enables efficient priority-based extraction, dynamic insertion, constant-time access to minimum, and efficient heapify operations.

4.**Diictory**
-Used for the hard coded data 

3. **Red-Black Tree (RedBlackTree.cs)**
 - Used to efficiently manage and search for service requests. Provides a balanced structure, fast searching, duplicate handling, and in-order traversal support.

## Project Files

- `MainWindow.xaml` & `MainWindow.xaml.cs`: Main window and navigation.
- `LocalEventsWindow.xaml` & `LocalEventsWindow.xaml.cs`: Local events management interface.
- `ReportIssuesWindow.xaml` & `ReportIssuesWindow.xaml.cs`: Interface for reporting issues.
- `ProvideFeedbackWindow.xaml` & `ProvideFeedbackWindow.xaml.cs`: Feedback submission interface.
- `AddEventWindow.xaml` & `AddEventWindow.xaml.cs`: Interface for adding/editing events.
- `ServiceRequestStatus.Xaml`& `ServiceRequestStatus.Xaml`:Service request ststus interface
- `Event.cs`: Class definition for event objects.
- `Graph.cs`: Implementation of the Graph data structure.
- `MinHeap.cs`: Implementation of the Min Heap data structure.
- `RedBlackTree.cs`: Implementation of the Red-Black Tree data structure.

## Technical Details 

- User interface is created with XAML, and backend logic is in C#.
- The project demonstrates the use of C# data structures and LINQ for optimized data handling.
- UI design follows a consistent color theme with gradient backgrounds.
- Backend implemented in C# with advanced data structures.
-XAML-based UI design
-Custom data structure implementations
-Object-oriented design patterns
-Efficient algorithms for data management
-Responsive user interface with gradient themes
-LINQ integration for data operations

## Techincal implementation: 
1.**Service Request Management Interface**
 - Search Functionality:
- Multiple search criteria support (ID, Description, Status, Priority, Location)
- Real-time search results
- Clear search capability

2.**Request Visualization**
- GridView-based request listing
- Status-based graphical representation
- Priority-based sorting
- Interface Features
- Responsive grid layout
- Visual feedback for user actions
- Efficient data presentation
- Advanced filtering capabilities

## Author

ST10083869

---

For any questions, suggestions, or contributions, contact the project maintainer or submit an issue via the project repository.
