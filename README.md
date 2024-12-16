# University Project Backend Documentation

## Overview
The University project backend is built using ASP.NET Core and provides functionalities for user management, questions, and answers.

## Directory Structure
- **Data**: Contains data-related files, including the database context and migrations.
  - **ApplicationDbContext.cs**: Defines the database context for the application, including DbSets for `Student`, `Question`, and `Answer`.
- **Endpoints**: Contains API endpoint definitions.
  - **IdentityEndpoint.cs**: Provides endpoints for user registration, login, and logout.
  - **QaEndpoint.cs**: Provides endpoints for managing questions and answers.

## Key Components
### Program.cs
- The main entry point for the application.
- Configures services, including OpenAPI, database context, and identity management.
- Sets up the HTTP request pipeline and maps custom identity and Q&A endpoints.

### ApplicationDbContext.cs
- Inherits from `IdentityDbContext<IdentityUser>`.
- Contains DbSets for managing `Student`, `Question`, and `Answer` entities.

### IdentityEndpoint.cs
- Adds identity management endpoints for user registration, login, and logout.
- Validates email addresses during registration.

### QaEndpoint.cs
- Provides endpoints for retrieving and submitting questions and answers.
- Requires user authentication for posting questions and answers.

## Installation Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/university-backend.git
   cd university-backend
   ```
2. Install the required packages:
   ```bash
   dotnet restore
   ```
3. Set up the database:
   ```bash
   dotnet ef database update
   ```

## Usage

To run the application, use the following command:
```bash
dotnet run
```

Access the API at `https://localhost:7172/api`. Check the documentation for available endpoints.

## Contributing

1. Fork the repository.
2. Create a new branch for your feature:
   ```bash
   git checkout -b feature/YourFeature
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add some feature"
   ```
4. Push to the branch:
   ```bash
   git push origin feature/YourFeature
   ```
5. Open a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
