# Hotel Management App

> A minimum value project for hotel management that allows guests to search for rooms based on certain dates and then book it, also employees can search for bookings and check-in guests.


## Concepts used
- OOP
- Dependency Injection
- ASP.NET Core Razor Pages
- WPF Core
- SQL Server
- SQLite

## Application structure
  - DataAccess ClassLibrary
    - DataAccess contains the business logic for CRUD operations and connects to the database directly.
  - Hotel Web Application
    - The web app is to allow guests to search and book rooms.
  - Hotel Desktop Application
    - The Desktop app is to allow employees to search for bookings and check-in guests.
