# Tool-App-Students

* This application is designed to enhance student collaboration and organization. It provides tools for managing schedules, facilitating group communication, and streamlining task assignments within academic teams.

## Table of Contents

* [Technologies Used](#technologies-used)
* [Features](#features)
    * [Calendar Page](#calendar-page)
* [Installation](#installation)
    * [Prerequisites](#prerequisites)
    * [Setup](#setup)
* [Usage](#usage)
    * [Calendar Page Usage](#calendar-page-usage)
* [Project Structure](#project-structure)
* [Future Enhancements](#future-enhancements)

## Technologies Used

* **Frontend:**
    * .NET MAUI (C#) - Cross-platform mobile and desktop UI framework
* **Backend:**
    * ASP.NET Core (C#) - Web API framework
* **Database:**
    * MySQL - Relational database management system
* **Other:**
    * RESTful APIs - For communication between frontend and backend

## Features

### Calendar Page

* Provides a visual interface for students to manage their personal and group schedules.
* Allows users to:
  * Create events with details like title, description, date, time, and location.
  * View events weekly.
        
##  Installation

###  Prerequisites

* **Operating System:** Windows 10 or later, Android 14.0 or later.
* **.NET SDK:** 8.0 or later
* **MySQL Server:** 8.0.39 or later

### Setup

1.  Clone the repository: `git clone https://github.com/BocaDoru/Tool-App-Students.git`
2.  **Backend Setup (ASP.NET Core API):**
    * Navigate to the `ToolAppStudentsServer/` directory.
    * Configure the database connection string in `appsettings.json` to match your MySQL setup.
    * Run database migrations to create the necessary tables.
    * Build and run the API.
3.  **Frontend Setup (.NET MAUI App):**
    * Navigate to the `ToolAppStudentsClient/` directory.
    * Configure the API endpoint URL to match the location of your running backend.
    * Build and run the application for your target platform (Android, Windows, macOS).

## Usage

### Calendar Page Usage

  **Creating Events:**
    * Click the "Add Event" button.
    * Fill in the event details: title, description, date, time, location.
    * Click "Save."

##   Project Structure

### Backend:

* `Program.cs`: Handles builder service configuration.
* **Context:**
  * `UserAuthContext.cs`: Handles Database access.
* **Controllers:**
  * `UserControllers.cs`: Handles API requests.
* **DTO:**
  * `LoginDto.cs`: Data transfer object for login.
  * `RegisterDto.cs`: Data transfer object for register.
  * `TaskDto.cs`: Data transfer object for tasks.
* **Models:**
  * `CalendarTask.cs`: Calendar task model.
  * `LoginResponse.cs`: Login response model.
  * `User.cs`: User model.
* **Service:**
  * `UserService.cs`: Handels database user services.

### Frontend:

* `MauiProgram.cs`: Handles builder service configuration.
* `IPlatformHttpMessageHandler.cs`: Handles HTTPS requests for different platforms.
* **ColorsTheme:**
  * `DarkBlueTheme.cs`: Dark-Blue theme colors.
* **Controls:**
  * `Calendar.xaml`: Calendar content view.
  * `DateCalendarCell.xaml`: Date calendar cell content view.
  * `EmptyCalendarCell.xaml`: Empty calendar cell content view.
  * `TaskCalendarCell.xaml`: Task calendar cell content view.
  * `TextCalendarCell.xaml`: Text calendar cell content view.
* **DTO:**
  * `LoginDto.cs`: Data transfer object for login.
  * `RegisterDto.cs`: Data transfer object for register.
  * `TaskDto.cs`: Data transfer object for tasks.
* **Models:**
  * `TaskModel.cs`: Calendar task model.
  * `TaskColorButton.cs`: Calendar task color model.
  * `User.cs`: User model.
* **Popups:**
  * `TaskPopup.xaml`: Popup for adding new tasks.
* **Response:**
  * `CalendarTaskResponse.cs`: Calendar task response model.
  * `LoginResponse.cs`: Login response model.
* **Services:**
  * `ApiClient.cs`: REST API request with or without authentication.
  * `CalendarServices.cs`: Calendar services.
  * `ClientServices.cs`: User services.
* **View:**
  * `LoginPage.xaml`: Login page view.
  * `RegisterPage.xaml`: Register page view.
  * `WeekCalendar.cs`: Week calendar page view.
* **ViewModel:**
  * `LoginViewModel.cs`: Login page viewmodel.
  * `RegisterViewModel.cs`: Register page viewmodel.
  * `TaskPopupViewModel.cs`: Task popup viewmodel.
  * `WeekCalendarViewModel.cs`: Week calendar page viewmodel.

## Future Enhancements

* A communication channel for general discussions within friends.
* A dedicated chat space for project teams to collaborate effectively.
* AI-powered task prioritization and scheduling suggestions.
* Push notifications for mobile platforms.
* **Gamification of features to increase student engagement:**
  * Implementation of a points and badge system for completing tasks and contributing to group activities.
  * Creation of leaderboards to foster friendly competition among students.
  * Visual progress tracking to show students achievements and encourage continued participation.
