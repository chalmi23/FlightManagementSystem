Flight Management System

Flight Management System (FMS) is a web-based application designed to manage flight information. The application provides a RESTful API built using ASP.NET Core, allowing users to perform CRUD (Create, Read, Update, Delete) operations on flight records.
Features:
- API for Flight Management: The API offers endpoints for adding, reading, updating, and deleting flight records.
- ORM Integration: Entity Framework Core is used as the ORM framework, simplifying data access and management.
- Authentication Mechanism: JWT is implemented for secure access to API endpoints. Users can create an account, log in, and manage flights using JWT tokens.

User Authentication API:
- Register: Endpoint to create a new user account.
- Login: Endpoint to authenticate users and generate JWT tokens.
- Manage Flights: Users can manage flights by including the JWT token in API requests.

Technologies Used:
- ASP.NET Core
- Entity Framework Core
- JWT Authentication
- Angular (frontend: https://github.com/chalmi23/FlightUserInterface)
- C#

License: MIT License
