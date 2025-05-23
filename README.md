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

## Technical Overview

### Tech Stack

#### Frontend
- **Framework**: React 18 with TypeScript
- **State Management**: React Hooks
- **Styling**: CSS Modules
- **Routing**: React Router v6
- **HTTP Client**: Axios
- **Build Tool**: Create React App
- **Package Manager**: npm

#### Backend
- **Framework**: .NET 8
- **Architecture**: Clean Architecture
- **ORM**: Entity Framework Core 8
- **Database**: SQL Server
- **API**: RESTful with ASP.NET Core Web API
- **Authentication**: JWT
- **Real-time Updates**: SignalR
- **File Storage**: Local file system (with interface for cloud storage)

#### Development Tools
- **IDE**: Visual Studio 2022
- **Version Control**: Git
- **Database Management**: SQL Server Management Studio
- **API Testing**: Swagger

### Architecture & Design Patterns

#### Core Design Patterns
- **Repository Pattern**
  - Abstracts and encapsulates data access logic
  - Separates business logic from data access
  - Improves maintainability and testability
  - Provides consistent data access interface

- **Strategy Pattern**
  - Enables flexible, plant-specific care recommendations
  - Encapsulates different care algorithms
  - Allows runtime strategy selection
  - Supports strategy sharing and adaptation

- **DTO Pattern**
  - Defines data transfer objects for API communication
  - Decouples internal models from API contracts
  - Improves security and versioning
  - Controls data exposure

- **Service Layer Pattern**
  - Organizes business logic into dedicated services
  - Centralizes business rules and validation
  - Improves maintainability and testability
  - Handles complex operations and orchestration

## Project Structure

### Backend Structure
```
PlantCareBuddy/
├── PlantCareBuddy.sln              # Visual Studio solution file
├── PlantCareBuddy.Domain/          # Domain entities and business rules
├── PlantCareBuddy.Application/     # Application services and business logic
├── PlantCareBuddy.Infrastructure/  # Data access and external services
└── PlantCareBuddy.API/             # Web API and UI hosting
```

### Frontend Structure
```
client/
├── public/                 # Static assets
├── src/                    # Source code
│   ├── api/                # API service layer
│   ├── components/         # Reusable UI components
│   ├── models/             # TypeScript interfaces
│   ├── pages/              # Page components
│   ├── utils/              # Helper functions
│   ├── App.tsx            # Main application component
│   ├── index.tsx          # Application entry point
│   └── [other config files]
├── package.json           # Dependencies and scripts
└── tsconfig.json         # TypeScript configuration
```

### Layer Responsibilities

#### Domain Layer
- Contains core business entities
- Defines business rules and validations
- Houses domain-specific exceptions
- Independent of other layers

#### Application Layer
- Contains business logic and use cases
- Defines DTOs for data transfer
- Houses service interfaces
- Manages object mapping
- Coordinates between domain and infrastructure

#### Infrastructure Layer
- Implements data access (EF Core)
- Handles external services
- Contains repository implementations
- Manages file storage
- Handles notifications

#### API Layer
- Exposes RESTful endpoints
- Handles HTTP requests/responses
- Manages authentication/authorization
- Hosts SignalR hubs
- Contains middleware

#### Frontend Layer
- Manages user interface
- Handles state management
- Communicates with backend API
- Provides real-time updates
- Manages routing and navigation

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
