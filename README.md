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

### Frontend (React + TypeScript)
- Modern, responsive UI
- Component-based architecture
- Type-safe development
- Clean and intuitive user interface

### Backend (.NET)
- Clean architecture implementation
- Domain-driven design
- RESTful API endpoints
- Strategy pattern for care recommendations

## Getting Started

### Prerequisites
- Node.js (for frontend)
- .NET SDK (for backend)
- SQL Server (or compatible database)

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
