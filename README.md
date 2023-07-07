# Survey Application
The Survey Application is a web-based application developed using ASP.NET Core 8 and C# 12 that allows users to create and answer surveys. It follows the repository and unit of work patterns and implements a clean architecture structure with the following components:

- **Core:**  It encapsulates the core functionality and business logic of the application.
- **Domain:** It represents the domain model and defines the contracts for interacting with repositories.
- **Infrastructure:** The Infrastructure folder contains components related to data access and infrastructure concerns.
- **Web.Api:** ASP.NET Core Web API project.
- **Web.UI:** ASP.NET Core MVC project.

# Technologies and Libraries
The Survey Application utilizes the following technologies and libraries:

- ASP.NET Core 8
- C# 12
- Entity Framework Core (EF Core)
- Microsoft SQL Server
- ASP.NET Identity
- AutoMapper
- Swagger
- JWT
- SweetAlert2
- DataTables
- Google Charts
- Material Design for Bootstrap
- Bootstrap
- jQuery
- FontAwesome

# Features
- **Account Creation:** Users can create an account through the API.
- **Survey Creation:** Users can create surveys via the API, which generates a URL for answering the survey.
- **Survey Management:** Optional functionality to retrieve, update, and delete surveys through the API.
- **Survey Answering:** Users can answer surveys in the MVC project without authentication, allowing multiple submissions for anonymity.
- **Dashboard and Survey Statistics:** Authenticated users can access the MVC project's dashboard to view survey statistics.

# Getting Started
To run the Survey Application locally, follow these steps:

- Clone the repository.
- Install .NET 8 and C# 12.
- Set up the required dependencies, including EF Core, MsSql, and other libraries.
- Build and run the API and MVC projects.

# Feedback and Contributions
Feedback and contributions are welcome! If you have any questions, suggestions, or bug reports, please reach out to me. Contributions to enhance the application are highly appreciated.

# License
The Survey Application is open-source and released under the MIT License.




