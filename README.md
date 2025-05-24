# Plant Care Buddy 🌱

A digital plant care management system designed to help users track and maintain their houseplant collection. It enables plant enthusiasts to monitor plant health, schedule care activities, document growth with photos, and receive timely care reminders.

## Features

### Plant Management
- Plant listing and details view
- Add and edit plant information
- Plant type classification
- Photo management for plants

### Health Tracking
- Add and edit health observations
- Track plant health status
- Document plant issues and improvements
- Health history tracking

### Care Events
- Record care activities (watering, fertilizing, etc.)
- Edit care event details
- View care history
- Track care event dates

### Care Recommendations
- Plant-specific care strategies
- Seasonal care adjustments
- Watering frequency recommendations
- Fertilizing schedule
- Light and humidity recommendations
- Next care dates calculation

## Project Structure

### Frontend
- **Framework**: React 18 with TypeScript
- **State Management**: React Hooks
- **Styling**: CSS Modules
- **Routing**: React Router v6
- **HTTP Client**: Axios
- **Build Tool**: Create React App
- **Package Manager**: npm

### Backend
- **Framework**: .NET 8
- **Architecture**: Clean Architecture
- **ORM**: Entity Framework Core 8
- **Database**: SQL Server
- **API**: RESTful with ASP.NET Core Web API
- **Authentication**: JWT (planned)
- **File Storage**: Local file system (with interface for cloud storage)
- **Design Patterns**:
  - Repository Pattern
    - The repository pattern is used to abstract and encapsulate all data access logic for each main entity (such as Plant, CareEvent, GrowthPhoto, Reminder, etc.). This approach separates business logic from data access, making the codebase more maintainable, testable, and scalable. Repository interfaces are defined in the domain layer and implemented in the infrastructure layer, ensuring that services and controllers interact with data through clean, consistent interfaces rather than direct database queries.
  - Strategy Pattern (for care recommendations)
    - The strategy pattern enables Plant Care Buddy to provide flexible, plant-specific care recommendations by encapsulating different care algorithms (such as default, succulent, or tropical strategies) as interchangeable components. This allows the system to select and apply the most appropriate care logic for each plant type or condition, making the application extensible and adaptable to new care approaches without changing core logic.
  - DTO Pattern
    - The Data Transfer Object (DTO) pattern is used to define simple, serializable objects for transferring data between the backend and frontend (or between layers of the backend). This pattern helps decouple the internal domain models from the data exposed via APIs, improving security, versioning, and maintainability. DTOs ensure that only the necessary data is sent or received, and that changes to internal models do not directly impact API contracts.
  - Service Layer Pattern
    - The service layer pattern organizes business logic into dedicated service classes, which act as intermediaries between controllers (API endpoints) and repositories (data access). This pattern centralizes and encapsulates business rules, validation, and orchestration, making the codebase easier to maintain, test, and extend. In Plant Care Buddy, services handle operations such as care recommendation generation, plant management, and care event processing.

### Development Tools
- **IDE**: Visual Studio 2022
- **Version Control**: Git
- **Database Management**: SQL Server Management Studio
- **API Testing**: Swagger


## Getting Started

### Prerequisites
- Node.js 18+ (for frontend)
- .NET 8 SDK (for backend)
- SQL Server 2022 (or compatible database)
- Visual Studio 2022 (recommended)

### Frontend Setup
```bash
cd client
npm install
npm start
```

### Backend Setup
```bash
cd PlantCareBuddy
dotnet restore
dotnet run
```

## Development Status

### Completed Features
- Core plant management system
- Health tracking and observations
- Care event management
- Photo management
- Care recommendation system with:
  - Default care strategy
  - Succulent care strategy
  - Tropical plant care strategy
  - Seasonal adjustments

### In Progress
- Care planning and scheduling
- User authentication and authorization
- Analytics and insights
- Mobile experience optimization

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.
