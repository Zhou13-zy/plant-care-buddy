# Plant Buddy: Agile Development Plan

This document outlines the agile development plan for the Plant Buddy application, organized by epics and tasks.

## Project Structure

The Plant Buddy application follows a clean architecture approach with clear separation of concerns. The solution is organized into the following projects:

### Backend Structure

```
PlantBuddy/                     ← Solution root
├── PlantBuddy.sln              ← Visual Studio solution file
├── PlantBuddy.Domain/          ← Domain entities and business rules
│   ├── Entities/               ← Core domain entities (Plant, CareEvent, etc.)
│   │   ├── Enums/              ← Enumeration types
│   │   └── Exceptions/         ← Domain-specific exceptions
│   │
│   ├── PlantBuddy.Application/     ← Application services and business logic
│   │   ├── DTOs/                   ← Data Transfer Objects
│   │   │   ├── Plants/             ← Plant-related DTOs
│   │   │   ├── CareEvents/         ← Care event DTOs
│   │   │   └── Photos/             ← Photo-related DTOs
│   │   ├── Interfaces/             ← Service and repository interfaces
│   │   │   ├── Repositories/       ← Repository interfaces
│   │   │   └── Services/           ← Service interfaces
│   │   ├── Services/               ← Service implementations
│   │   └── Mapping/                ← Object mapping configurations
│   │
│   ├── PlantBuddy.Infrastructure/  ← Data access and external services
│   │   ├── Data/                   ← Database related code
│   │   │   ├── Context/            ← EF Core DbContext
│   │   │   ├── Repositories/       ← Repository implementations
│   │   │   └── Configurations/     ← Entity configurations
│   │   ├── Services/               ← Infrastructure service implementations
│   │   │   ├── Storage/            ← File/image storage
│   │   │   └── Notifications/      ← Notification services
│   │   └── Migrations/             ← EF Core database migrations
│   │
│   └── PlantBuddy.API/             ← Web API and UI hosting
│       ├── Controllers/            ← API controllers
│       ├── Hubs/                   ← SignalR hubs
│       ├── Middleware/             ← Custom middleware
│       ├── Extensions/             ← Service collection extensions
│       ├── Filters/                ← Action filters
│       └── Program.cs              ← Application entry point and configuration
```

### Frontend Structure

```
client/                     ← Frontend application root
├── public/                 ← Static assets
├── src/                    ← Source code
│   ├── api/                ← API service layer
│   │   ├── axiosConfig.ts  ← API client configuration
│   │   ├── plantService.ts ← Plant API methods
│   │   ├── careService.ts  ← Care events API methods
│   │   └── photoService.ts ← Photo/timeline API methods
│   │
│   ├── components/         ← Reusable UI components
│   │   ├── plants/         ← Plant-related components
│   │   ├── care/           ← Care-related components
│   │   ├── photos/         ← Photo components
│   │   ├── common/         ← Shared components
│   │   └── layout/         ← Layout components
│   │
│   ├── contexts/           ← React context providers
│   │   ├── PlantContext.tsx     ← Plant data provider
│   │   ├── CareContext.tsx      ← Care data provider 
│   │   └── NotificationContext.tsx ← Real-time notifications
│   │
│   ├── hooks/              ← Custom React hooks
│   │   ├── usePlants.ts    ← Plant data hook
│   │   ├── useCareEvents.ts ← Care events hook
│   │   └── useSignalR.ts   ← SignalR connection hook
│   │
│   ├── models/             ← TypeScript interfaces and types
│   │   ├── plant.ts        ← Plant-related types
│   │   ├── careEvent.ts    ← Care event types
│   │   └── photo.ts        ← Photo/timeline types
│   │
│   ├── pages/              ← Page components
│   │   ├── Dashboard.tsx   ← Main dashboard
│   │   ├── PlantsPage.tsx  ← Plants listing
│   │   ├── PlantDetailPage.tsx ← Plant details
│   │   └── TimelinePage.tsx ← Growth timeline
│   │
│   ├── utils/              ← Helper functions and utilities
│   ├── App.tsx             ← Main application component
│   └── index.tsx           ← Application entry point
│
├── package.json            ← Dependencies and scripts
└── tsconfig.json           ← TypeScript configuration
```

### Tests Structure

```
tests/                      ← Test projects
├── PlantBuddy.Domain.Tests/      ← Domain layer tests
├── PlantBuddy.Application.Tests/ ← Application layer tests
├── PlantBuddy.Infrastructure.Tests/ ← Infrastructure layer tests
├── PlantBuddy.API.Tests/         ← API integration tests
└── client/                       ← Frontend tests
    ├── components/               ← Component tests
    ├── hooks/                    ← Custom hooks tests
    └── pages/                    ← Page component tests
```

This structure promotes:
- Clear separation of concerns
- Domain-driven design principles
- Testability across all layers
- Independent evolution of UI and API
- Reduced coupling between components
- Scalability as the application grows

## Epic 1: Core Infrastructure Setup
**Description:** Establish the foundational architecture for the Plant Buddy application

### Task 1.1: Set up ASP.NET Core Web API project with basic configurations
**Purpose:** Establish the foundation of the backend application by creating a properly configured API that will serve as the communication layer between the frontend and the database, ensuring secure, documented, and standardized API endpoints.

**Requirements:**
- Create ASP.NET Core Web API project using .NET 8
- Configure CORS to allow frontend connections
- Set up Swagger/OpenAPI documentation
- Configure middleware pipeline (exception handling, logging)
- Establish dependency injection container

**Technology:**
- .NET 8
- ASP.NET Core Web API
- Swagger/OpenAPI
- CORS middleware

**Design Patterns:**
- Dependency Injection (built into ASP.NET Core)
- Middleware Pattern for request pipeline
- Options Pattern for configuration

**Output:**
- Functioning API project that responds to basic requests
- Configured Program.cs with middleware chain
- Swagger UI accessible at /swagger endpoint
- CORS policy allowing frontend access

**Reference:** README.md Section 8.1 (Logical Diagram) describes the ASP.NET Core Web API layer as part of the architecture.

### Task 1.2: Create EF Core DbContext with initial Plant entity
**Purpose:** Build the data access foundation that will enable persistent storage and retrieval of plant information while establishing proper entity relationships and constraints that maintain data integrity.

**Requirements:**
- Implement PlantBuddyContext inheriting from DbContext
- Create initial Plant entity model with core properties
- Configure entity relationships and constraints
- Set up DbSet properties for main entities

**Technology:**
- Entity Framework Core 8
- SQL Server Provider
- Fluent API for configurations

**Design Patterns:**
- Repository Pattern foundation
- Unit of Work (implicit in DbContext)
- Domain Model pattern for entities
- Fluent Interface for configurations

**Output:**
- PlantBuddyContext.cs in Infrastructure layer
- Initial Plant.cs model in Domain layer
- Entity configurations using Fluent API
- Configured relationships between entities

**Reference:** README.md Section 6 (Database Schema) outlines the Plants table and its relationships.

### Task 1.3: Configure database connection and create initial migration
**Purpose:** Set up reliable, environment-aware database connectivity and version control for the database schema, allowing for consistent data storage across different deployment environments.

**Requirements:**
- Set up connection string configuration
- Configure DbContext in service container
- Create initial EF Core migration
- Test database creation and connection
- Set up environment-specific configurations

**Technology:**
- EF Core Migrations
- SQL Server
- .NET Configuration API

**Design Patterns:**
- Configuration Management patterns
- Environment-based configuration

**Output:**
- Working database connection
- Initial migration file creating Plants table
- Successful database creation
- Configuration files for different environments

**Reference:** README.md Section 13 (Database Migration Guide) provides detailed migration guidance.

### Task 1.4: Initialize React frontend with TypeScript and routing setup
**Purpose:** Create the foundation for the user interface with type safety, navigation capabilities, and communication with the backend, providing the structure for a maintainable and scalable frontend application.

**Requirements:**
- Create React application with TypeScript
- Set up routing infrastructure with React Router
- Configure API client with Axios
- Establish basic component structure
- Set up state management foundation

**Technology:**
- React a18 with TypeScript
- React Router
- Axios for API communication
- CSS/SCSS for styling

**Design Patterns:**
- Component-Based Architecture
- Container/Presentational pattern
- Custom hooks pattern
- Single Responsibility Principle for components

**Output:**
- Functioning React application
- Router configuration with main routes
- API client with base URL and interceptors
- Placeholder main pages and components

**Reference:** README.md Section 14.1 outlines the client directory structure.

### Task 1.5: Create Plant entity with essential properties
**Purpose:** Define the core domain model for plants with proper validation and relationships, establishing the central entity around which the application's functionality will be built.

**Requirements:**
- Implement complete Plant entity with all required properties
- Add data annotations for validation
- Configure relationships to other entities
- Implement PlantHealthStatus enum
- Document properties with XML comments

**Technology:**
- C# Entity classes
- EF Core Attributes
- XML documentation

**Design Patterns:**
- Rich Domain Model
- Value Objects for complex properties
- Enums for fixed value sets

**Output:**
- Complete Plant.cs model with properties listed in README
- PlantHealthStatus.cs enum
- Navigation properties for future relationships
- Property validation attributes

**Reference:** README.md Section 3.1 (Plant Management) details the required plant properties.

### Task 1.6: Implement GET endpoint for retrieving all plants
**Purpose:** Develop the first functional API endpoint that allows the frontend to access plant data, setting patterns for future API development while providing essential data retrieval capabilities.

**Requirements:**
- Create PlantController with GET endpoint
- Implement service method to retrieve plants
- Map domain entities to DTOs
- Add basic filtering capabilities
- Handle exceptions and return appropriate status codes

**Technology:**
- ASP.NET Core Controllers
- AutoMapper (optional)
- Action filters

**Design Patterns:**
- DTO pattern
- Repository pattern
- Service pattern
- Adapter pattern for mapping

**Output:**
- PlantController with GET /api/plants endpoint
- PlantDto for API responses
- Service method implementation
- Repository method for querying plants

**Reference:** README.md Section 3.1 outlines Plant Management requirements and Section 8.3 shows the component interaction flow.

### Task 1.7: Build PlantsList component with basic styling
**Purpose:** Create the primary user interface component for displaying plant collections, laying the groundwork for how users will visualize and interact with their plants in the application.

**Requirements:**
- Create React component to display plant collection
- Implement grid/list view with responsive design
- Display core plant information (name, species, health status)
- Add loading state and error handling
- Implement basic styling with CSS/SCSS

**Technology:**
- React functional components
- CSS/SCSS or styling library
- Responsive design techniques
- React hooks (useState, useEffect)

**Design Patterns:**
- Compound Component pattern
- Container/Presentational pattern
- Error Boundary pattern
- Custom hooks for data fetching

**Output:**
- PlantsList.tsx component
- PlantCard.tsx subcomponent
- Styling for plant cards/list
- Loading and error states

**Reference:** README.md Section 14.2.4 outlines component structure including display components.

### Task 1.8: Add plant service for API communication
**Purpose:** Establish a type-safe, reliable communication layer between the frontend and backend API, centralizing API call logic and providing consistent error handling for plant-related operations.

**Requirements:**
- Create service for plant-related API calls
- Implement error handling and retry logic
- Define TypeScript interfaces matching DTOs
- Set up state management for plant data
- Add logging for API communication

**Technology:**
- TypeScript
- Axios or Fetch API
- React Context (or other state management)
- TypeScript interfaces

**Design Patterns:**
- Service Layer pattern
- Adapter pattern for API responses
- Error handling patterns
- Repository pattern (client-side)

**Output:**
- plantService.ts with API methods
- Plant interfaces/types
- Context provider for plant state
- Error handling utilities

**Reference:** README.md Section 14.2.1 describes the API layer including the plantService.ts file.

## Epic 2: Plant Management
**Description:** Implement complete functionality for users to add, view, edit, and delete plants

### Task 2.1: Create DTO for plant creation
**Purpose:** Design a structured data transfer object that defines the contract between frontend and backend for plant creation, ensuring proper validation and data integrity when users add new plants to the system.

**Requirements:**
- Design CreatePlantDto with all necessary properties
- Implement validation attributes for required fields
- Document properties with XML comments
- Ensure DTO properties match frontend requirements

**Technology:**
- C# DTOs
- Data Annotations for validation
- XML documentation

**Design Patterns:**
- DTO (Data Transfer Object) pattern
- Validation patterns
- Contract-first design

**Output:**
- CreatePlantDto.cs in Application layer
- Validation rules for required fields
- Documentation for each property

**Reference:** README.md Section 8.3 shows the component interaction flow including DTOs.

### Task 2.2: Implement POST endpoint for adding plants
**Purpose:** Create the API endpoint that allows users to add new plants to their collection, validating input data and persisting plants to the database while providing appropriate feedback on the operation's success or failure.

**Requirements:**
- Create POST action in PlantController
- Validate incoming CreatePlantDto
- Map DTO to domain entity
- Save entity to database using repository
- Return appropriate status codes and created plant

**Technology:**
- ASP.NET Core Controllers
- Model binding
- AutoMapper (optional)

**Design Patterns:**
- DTO pattern
- Repository pattern
- Service pattern
- PRG (Post-Redirect-Get) pattern

**Output:**
- POST endpoint in PlantController
- Service method for creating plants
- Repository implementation for adding plants
- Proper error handling and validation

**Reference:** README.md Section 8.3 details the component interaction flow for creating a plant.

### Task 2.3: Build PlantForm component for data entry
**Purpose:** Provide users with an intuitive, validated form interface for entering plant information, making it easy to add new plants to their collection while ensuring data quality.

**Requirements:**
- Create React form component for plant data entry
- Implement form controls for all plant properties
- Add client-side validation
- Handle form submission and API communication
- Provide feedback on success/error

**Technology:**
- React functional components
- React Hook Form or Formik (optional)
- CSS/SCSS for styling
- React hooks (useState, useEffect, useContext)

**Design Patterns:**
- Controlled Components pattern
- Form Builder pattern
- Adapter pattern for API communication
- Error handling patterns

**Output:**
- PlantForm.tsx component
- Form validation rules
- Success/error messaging
- Integration with plant service

**Reference:** README.md Section 14.2.4 mentions form components including PlantForm.tsx.

### Task 2.4: Add form validation and submission handling
**Purpose:** Enhance the user experience by providing immediate feedback on form inputs, preventing invalid data submission, and clearly communicating the status of plant creation operations.

**Requirements:**
- Implement comprehensive client-side validation
- Add server-side validation error display
- Create loading state during submission
- Handle successful submission with feedback
- Implement error recovery strategies

**Technology:**
- React validation libraries
- Form state management
- Toast notifications or alerts

**Design Patterns:**
- Strategy pattern for validation
- Observer pattern for form state
- Command pattern for submission
- Decorator pattern for validation rules

**Output:**
- Validation rules for all form fields
- Loading indicators
- Success messaging
- Error display and recovery

**Reference:** README.md Section 9.7 (Seasonal Adjustments) mentions the need for validation in plant care inputs.

### Task 2.5: Implement GET endpoint for single plant details
**Purpose:** Create the API endpoint that retrieves comprehensive information about a specific plant, including related data, enabling detailed plant views and management in the user interface.

**Requirements:**
- Create GET action for retrieving single plant by ID
- Include related data (care events, photos, etc.)
- Handle not found cases
- Return plant details with status code
- Document API endpoint

**Technology:**
- ASP.NET Core Controllers
- Entity Framework Include/ThenInclude
- API documentation tools

**Design Patterns:**
- Repository pattern
- DTO pattern
- Eager/Lazy loading strategies
- Adapter pattern for mapping

**Output:**
- GET /api/plants/{id} endpoint
- Service method for fetching plant details
- DTO including related data
- Swagger documentation

**Reference:** README.md Section 9.1 (Plant Collection Management) outlines the detailed information needed for plant details.

### Task 2.6: Create PlantDetailPage component
**Purpose:** Build the primary interface for viewing and managing individual plants, providing users with a detailed view of all plant information and related data in an organized, user-friendly layout.

**Requirements:**
- Build page component for displaying plant details
- Fetch plant data from API
- Display all plant information with proper formatting
- Add loading and error states
- Create layout for related data sections

**Technology:**
- React components
- React Router for parameters
- CSS/SCSS for layout
- React hooks (useState, useEffect, useParams)

**Design Patterns:**
- Container/Presentational pattern
- Custom hooks for data fetching
- Error boundary pattern
- Adapter pattern for data transformation

**Output:**
- PlantDetailPage.tsx component
- Layout structure for details
- Loading and error handling
- Navigation integration

**Reference:** README.md Section 14.2.5 describes page components including PlantDetailPage.

### Task 2.7: Add routing to detail page from list
**Purpose:** Create seamless navigation between the plant list and detail views, allowing users to explore specific plant information while maintaining context and enabling easy navigation back to the list.

**Requirements:**
- Implement routing from plant list to detail view
- Handle URL parameters for plant ID
- Add navigation elements in UI
- Preserve state during navigation
- Handle browser history and back button

**Technology:**
- React Router
- History API
- URL parameter handling

**Design Patterns:**
- Router pattern
- Observer pattern for navigation events
- State persistence patterns

**Output:**
- Updated routing configuration
- Navigation links on plant cards
- URL structure with plant IDs
- Navigation controls for returning to list

**Reference:** README.md Section 14.1 mentions the directory structure including routing setup.

### Task 2.8: Display complete plant information with styling
**Purpose:** Create a visually appealing and informative plant detail page that presents all relevant plant information in a well-organized, user-friendly layout that adapts to different screen sizes.

**Requirements:**
- Create styled layout for all plant details
- Display plant photo prominently
- Format data fields appropriately
- Implement responsive design for all screen sizes
- Add visual indicators for health status

**Technology:**
- CSS/SCSS for styling
- Responsive design techniques
- Image optimization

**Design Patterns:**
- Presentational Components
- Responsive design patterns
- UI Component composition

**Output:**
- Fully styled plant detail view
- Responsive layout
- Visual health indicators
- Properly formatted data fields

**Reference:** README.md Section 9.1 describes the detailed information needed for plant display.

### Task 2.9: Create DTO for plant updates
**Purpose:** Design a data transfer object specifically for plant updates that enables partial modifications while maintaining data validation, providing a clear contract for how plants can be modified in the system.

**Requirements:**
- Design UpdatePlantDto with necessary properties
- Include validation rules for update operations
- Document properties with XML comments
- Ensure it captures all updatable fields

**Technology:**
- C# DTOs
- Data Annotations for validation
- XML documentation

**Design Patterns:**
- DTO pattern
- Validation patterns
- Partial Update pattern

**Output:**
- UpdatePlantDto.cs in Application layer
- Validation rules
- Documentation

**Reference:** README.md Section 8.3 shows the component interaction flow including DTOs.

### Task 2.10: Implement PUT endpoint for updating plants
**Purpose:** Create the API endpoint that allows users to modify existing plants, correctly handling state transitions and persistence while maintaining data integrity throughout the update process.

**Requirements:**
- Create PUT action in PlantController
- Validate incoming UpdatePlantDto
- Load existing plant and apply updates
- Save changes to database
- Return appropriate status codes and updated plant

**Technology:**
- ASP.NET Core Controllers
- Entity Framework tracking
- AutoMapper (optional)

**Design Patterns:**
- Repository pattern
- Unit of Work pattern
- Command pattern
- Strategy pattern for updates

**Output:**
- PUT /api/plants/{id} endpoint
- Service method for updating plants
- Repository implementation
- Proper error handling

**Reference:** README.md Section 3.1 mentions the need to view, edit, and delete plant information.

### Task 2.11: Adapt PlantForm for edit functionality
**Purpose:** Enhance the plant form to support both creation and editing workflows, providing users with a consistent interface for managing plant information regardless of the operation type.

**Requirements:**
- Modify PlantForm to support editing existing plants
- Populate form with current plant data
- Handle updates vs. creation differently
- Validate edited fields
- Support partial updates

**Technology:**
- React components
- Form libraries
- React hooks

**Design Patterns:**
- Strategy pattern for form submission
- Adapter pattern for data transformation
- Command pattern for operations

**Output:**
- Enhanced PlantForm component
- Pre-population of form fields
- Edit-specific validation
- Update service integration

**Reference:** README.md Section 14.2.4 mentions form components including PlantForm.tsx.

### Task 2.12: Add update confirmation and error handling
**Purpose:** Improve user confidence when making changes to plant data by providing clear confirmations, meaningful error messages, and recovery options when updates don't proceed as expected.

**Requirements:**
- Create confirmation dialogues for updates
- Implement comprehensive error handling
- Provide user feedback on success
- Add recovery options for failures
- Log errors for debugging

**Technology:**
- Modal components
- Toast notifications
- Error logging

**Design Patterns:**
- Observer pattern for notifications
- Strategy pattern for error handling
- Command pattern with undo capability

**Output:**
- Confirmation dialogs
- Success notifications
- Error displays with recovery options
- Error logging

**Reference:** README.md Section 7 (Non-Functional Requirements) mentions reliability and usability needs.

### Task 2.13: Implement DELETE endpoint for plants
**Purpose:** Create the API endpoint for permanently removing plants from the collection, ensuring proper cleanup of related data and providing appropriate status information to the client.

**Requirements:**
- Create DELETE action in PlantController
- Verify plant existence before deletion
- Handle cascading deletes for related entities
- Return appropriate status codes
- Document API endpoint

**Technology:**
- ASP.NET Core Controllers
- Entity Framework delete operations
- Database transactions

**Design Patterns:**
- Repository pattern
- Unit of Work pattern
- Transaction Script pattern

**Output:**
- DELETE /api/plants/{id} endpoint
- Service method for deleting plants
- Repository implementation
- Proper error handling

**Reference:** README.md Section 3.1 mentions the need to delete plant information.

### Task 2.14: Add delete confirmation dialog
**Purpose:** Prevent accidental data loss by implementing a clear confirmation process for plant deletion, ensuring users understand the consequences of the action before proceeding.

**Requirements:**
- Create modal dialog for delete confirmation
- Explain consequences of deletion
- Provide cancel and confirm options
- Add safety measures to prevent accidental deletion
- Style dialog to indicate destructive action

**Technology:**
- React modal components
- CSS for styling
- State management for dialog

**Design Patterns:**
- Modal dialog pattern
- Command pattern with confirmation
- Decorator pattern for safety checks

**Output:**
- DeleteConfirmation.tsx component
- Styled modal dialog
- Safety measures (e.g., typing plant name)
- Clear consequence explanation

**Reference:** README.md Section 9.1 implies the need for responsible management of plant data.

### Task 2.15: Implement delete functionality in UI
**Purpose:** Integrate plant deletion capabilities into the user interface, providing an intuitive way for users to remove plants while handling the entire process from confirmation to success notification.

**Requirements:**
- Add delete button to plant detail view
- Connect button to confirmation dialog
- Handle API communication for deletion
- Manage state updates after successful deletion
- Handle errors during deletion

**Technology:**
- React components
- API service integration
- State management

**Design Patterns:**
- Command pattern
- Observer pattern for state updates
- Strategy pattern for navigation after deletion

**Output:**
- Delete button component
- Integration with confirmation dialog
- API call implementation
- Post-deletion navigation

**Reference:** README.md Section 3.1 mentions the need to delete plant information from the UI.

### Task 2.16: Update list view after deletion
**Purpose:** Ensure a seamless user experience after plant deletion by immediately reflecting changes in the plant list, maintaining UI consistency with the database state.

**Requirements:**
- Remove deleted plants from list view
- Update state after successful deletion
- Provide feedback on successful deletion
- Support undo functionality (optional)
- Refresh data if needed

**Technology:**
- State management
- Toast notifications
- Real-time updates (optional)

**Design Patterns:**
- Observer pattern
- Memento pattern (for undo)
- Command pattern with undo capability

**Output:**
- Updated list state management
- Success notification
- Optimistic UI updates
- Undo capability (optional)

**Reference:** README.md Section 3.3 mentions visual indicators for plant status changes.

### Task 2.17: Implement search endpoints with filtering
**Purpose:** Enable users to quickly find specific plants using various search criteria, allowing them to efficiently manage larger plant collections by providing powerful, flexible search capabilities that work seamlessly across the application.

**Requirements:**
- Create search/filter endpoint for plants
- Support multiple filter criteria (name, species, health)
- Implement pagination and sorting
- Optimize query performance
- Document API endpoint

**Technology:**
- ASP.NET Core query string binding
- LINQ for filtering and sorting
- Entity Framework optimizations

**Design Patterns:**
- Specification pattern
- Chain of Responsibility for filters
- Builder pattern for queries

**Output:**
- GET /api/plants with query parameters
- Service methods for filtering
- Pagination implementation
- Sorting options

**LINQ Implementation Example:**
```csharp
// Controller action demonstrating LINQ filtering, sorting, and pagination
[HttpGet]
public async Task<ActionResult<PagedResult<PlantDto>>> GetPlants(
    [FromQuery] string? searchTerm, 
    [FromQuery] PlantHealthStatus? healthStatus,
    [FromQuery] string? location,
    [FromQuery] string? sortBy = "Name",
    [FromQuery] string? sortDirection = "asc",
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
{
    // Start with IQueryable for deferred execution
    var query = _context.Plants.AsQueryable();
    
    // Apply filters conditionally using LINQ
    if (!string.IsNullOrEmpty(searchTerm))
    {
        searchTerm = searchTerm.ToLower();
        query = query.Where(p => p.Name.ToLower().Contains(searchTerm) || 
                                p.Species.ToLower().Contains(searchTerm));
    }
    
    if (healthStatus.HasValue)
    {
        query = query.Where(p => p.HealthStatus == healthStatus.Value);
    }
    
    if (!string.IsNullOrEmpty(location))
    {
        query = query.Where(p => p.Location == location);
    }
    
    // Get total count for pagination
    var totalCount = await query.CountAsync();
    
    // Apply dynamic sorting
    query = sortDirection.ToLower() == "desc"
        ? query.OrderByDynamic(sortBy, true)
        : query.OrderByDynamic(sortBy, false);
    
    // Apply pagination
    var paginatedResults = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .Select(p => new PlantDto
        {
            Id = p.Id,
            Name = p.Name,
            Species = p.Species,
            HealthStatus = p.HealthStatus,
            Location = p.Location,
            AcquisitionDate = p.AcquisitionDate,
            PhotoUrl = p.PrimaryImagePath,
            CareEventCount = p.CareEvents.Count,
            LastWateredDate = p.CareEvents
                .Where(ce => ce.EventType == CareEventType.Watering)
                .OrderByDescending(ce => ce.EventDate)
                .Select(ce => ce.EventDate)
                .FirstOrDefault()
        })
        .ToListAsync();
    
    // Return paginated result with metadata
    return new PagedResult<PlantDto>
    {
        Items = paginatedResults,
        TotalCount = totalCount,
        Page = page,
        PageSize = pageSize,
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
    };
}
```

**Reference:** README.md Section 3.1 mentions the need to filter and search the plant collection.

### Task 2.18: Build SearchBar component
**Purpose:** Provide an intuitive, accessible interface for users to search their plant collection, enabling quick information retrieval through an easy-to-use component that enhances the overall usability of the application.

**Requirements:**
- Create search input component
- Implement real-time search capabilities
- Add clear button functionality
- Style search bar for visibility
- Support search history (optional)

**Technology:**
- React components
- Debounced input
- CSS for styling

**Design Patterns:**
- Controlled component pattern
- Debounce pattern
- Command pattern for search execution

**Output:**
- SearchBar.tsx component
- Styled search input
- Clear button
- Integration with search logic

**Reference:** README.md Section 14.2.4 mentions utility components including SearchBar.tsx.

### Task 2.19: Add filter controls for location, species, health
**Purpose:** Allow users to narrow down their plant collection by specific attributes, making it easier to manage large collections and find plants based on meaningful groupings and characteristics.

**Requirements:**
- Create filter control components
- Support multiple selection criteria
- Implement filter reset functionality
- Update URL with filter parameters
- Preserve filters during navigation

**Technology:**
- React components
- URL query parameters
- State management for filters

**Design Patterns:**
- Composite pattern for filter groups
- Strategy pattern for different filter types
- Observer pattern for filter changes

**Output:**
- FilterControls.tsx component
- Individual filter components
- URL synchronization
- Reset functionality

**Reference:** README.md Section 3.1 mentions the need to filter the plant collection by various criteria.

### Task 2.20: Implement client-side quick filtering
**Purpose:** Enhance application responsiveness by performing lightweight filtering operations on the client side, providing instant feedback to users without requiring server roundtrips for common filtering scenarios.

**Requirements:**
- Add client-side filtering for faster response
- Implement text highlighting for search terms
- Support case-insensitive searching
- Add sorting capabilities
- Ensure accessibility for filter controls

**Technology:**
- JavaScript array methods
- Text highlighting libraries
- Accessible form controls

**Design Patterns:**
- Strategy pattern for filter implementations
- Decorator pattern for result highlighting
- Chain of Responsibility for filter pipeline

**Output:**
- Client-side filtering implementation
- Highlighted search results
- Sorting controls
- Accessible filter UI

**Reference:** README.md Section 7 mentions usability requirements for non-technical users.

### Task 2.21: Implement basic LINQ queries for plant management
**Purpose:** Establish efficient LINQ usage patterns for data access that enhance application performance and maintainability while keeping the implementation straightforward and accessible.

**Requirements:**
- Implement proper LINQ queries for common data operations
- Create extension methods for frequently used queries
- Ensure efficient database access through optimized LINQ usage
- Focus on readable, maintainable query patterns
- Implement pagination for large result sets

**Technology:**
- LINQ to Entities for database queries
- Entity Framework Core 8
- Extension methods for query reuse

**Design Patterns:**
- Repository pattern with LINQ integration
- Extension methods for query reuse
- Fluent interface for query composition

**Output:**
- Optimized repository query implementations
- Basic extension methods for common queries
- Efficient data projections with Select
- Pagination implementation for large collections

**Reference:** PLANT_BUDDY_PRD.md Section 5.6.6 details basic LINQ implementation strategies for the application.

### Task 2.22: Create LINQ extension methods utility class
**Purpose:** Establish a set of reusable LINQ extensions that simplify common query patterns, promote code reuse, and ensure consistent, efficient database access throughout the application.

**Requirements:**
- Implement reusable LINQ extension methods for common queries
- Create pagination extension methods
- Add methods for filtering by plant properties
- Support sorting operations
- Keep methods simple and focused on common use cases

**Technology:**
- LINQ
- C# Extension Methods
- Entity Framework Core

**Design Patterns:**
- Extension Methods pattern
- Repository pattern integration

**Output:**
- QueryableExtensions.cs utility class
- Common filtering extensions
- Sorting helper methods
- Pagination extensions

**Implementation Example:**
```csharp
// QueryableExtensions.cs
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlantBuddy.Domain.Entities;
using PlantBuddy.Domain.Enums;

namespace PlantBuddy.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Creates a paged result from an IQueryable
        /// </summary>
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> query, 
            int page, 
            int pageSize)
        {
            var totalCount = await query.CountAsync();
            
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }
        
        /// <summary>
        /// Filter plants that need watering within a specified number of days
        /// </summary>
        public static IQueryable<Plant> NeedsWateringWithin(
            this IQueryable<Plant> query, 
            int days)
        {
            var cutoffDate = DateTime.Now.AddDays(days);
            return query.Where(p => p.NextWateringDate <= cutoffDate);
        }
        
        /// <summary>
        /// Filter plants by health status
        /// </summary>
        public static IQueryable<Plant> WithHealthStatus(
            this IQueryable<Plant> query, 
            PlantHealthStatus status)
        {
            return query.Where(p => p.HealthStatus == status);
        }
        
        /// <summary>
        /// Filter plants by location
        /// </summary>
        public static IQueryable<Plant> InLocation(
            this IQueryable<Plant> query, 
            string location)
        {
            if (string.IsNullOrEmpty(location))
                return query;
                
            return query.Where(p => p.Location == location);
        }
        
        /// <summary>
        /// Search plants by name or species
        /// </summary>
        public static IQueryable<Plant> Search(
            this IQueryable<Plant> query, 
            string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return query;
                
            searchTerm = searchTerm.ToLower();
            return query.Where(p => p.Name.ToLower().Contains(searchTerm) || 
                                   p.Species.ToLower().Contains(searchTerm));
        }
    }
}
```

**Reference:** PLANT_BUDDY_PRD.md Section 5.6.4 mentions Custom Extension Methods for optimizing and reusing LINQ queries.

### Task 2.23: Implement domain-specific LINQ query methods
**Purpose:** Create helpful, focused LINQ query methods that encapsulate common plant care query logic, making it easier to express domain-specific queries while maintaining clean and readable code.

**Requirements:**
- Design domain-specific extension methods for plant queries
- Implement care schedule query methods
- Create health status filtering methods
- Add location and species grouping methods
- Focus on practical, commonly needed query operations

**Technology:**
- LINQ
- C# Extension Methods
- Entity Framework Core

**Design Patterns:**
- Extension Methods pattern
- Repository pattern integration

**Output:**
- PlantQueryExtensions.cs with domain methods
- CareQueryExtensions.cs for care-related queries
- Documentation with usage examples

**Implementation Example:**
```csharp
// PlantQueryExtensions.cs
using System;
using System.Linq;
using PlantBuddy.Domain.Entities;
using PlantBuddy.Domain.Enums;

namespace PlantBuddy.Infrastructure.Extensions
{
    public static class PlantQueryExtensions
    {
        // Find plants that need care within a specified timeframe
        public static IQueryable<Plant> NeedingCareWithin(
            this IQueryable<Plant> query, 
            int days)
        {
            var cutoffDate = DateTime.Now.AddDays(days);
            return query.Where(p => 
                p.NextWateringDate <= cutoffDate ||
                p.NextFertilizingDate <= cutoffDate ||
                p.NextRepottingDate <= cutoffDate);
        }
        
        // Get plants by care difficulty level
        public static IQueryable<Plant> WithCareDifficulty(
            this IQueryable<Plant> query,
            CareDifficulty difficulty)
        {
            return query.Where(p => p.CareDifficulty == difficulty);
        }
        
        // Find plants that haven't received a specific care type recently
        public static IQueryable<Plant> NeedingCareOfType(
            this IQueryable<Plant> query,
            CareEventType careType,
            int daysSinceLastCare)
        {
            var cutoffDate = DateTime.Now.AddDays(-daysSinceLastCare);
            
            return query.Where(p => !p.CareEvents
                .Any(ce => ce.EventType == careType && 
                     ce.EventDate >= cutoffDate));
        }
        
        // Group plants by location with counts
        public static IQueryable<LocationSummary> GroupByLocation(
            this IQueryable<Plant> query)
        {
            return query
                .GroupBy(p => p.Location)
                .Select(g => new LocationSummary
                {
                    Location = g.Key,
                    PlantCount = g.Count(),
                    HealthyCount = g.Count(p => p.HealthStatus == PlantHealthStatus.Healthy),
                    AttentionCount = g.Count(p => 
                        p.HealthStatus == PlantHealthStatus.NeedsAttention)
                })
                .OrderByDescending(l => l.PlantCount);
        }
    }
}
```

## Epic 3: Plant Care Tracking
**Description:** Enable users to record care events and monitor plant health status

### Task 3.1: Create CareEvent entity and relationship to Plant
**Purpose:** Establish the foundation for tracking plant care activities by creating the data model for care events, enabling the core functionality of recording and analyzing plant care history.

**Requirements:**
- Design CareEvent entity with all necessary properties
- Create CareEventType enum for event categorization
- Establish relationship to Plant entity
- Add validation rules and constraints
- Document entity and relationships

**Technology:**
- C# Entity classes
- EF Core relationships
- Data Annotations

**Design Patterns:**
- Rich Domain Model
- Entity-Relationship pattern
- Value Objects for complex properties
- Enumeration pattern

**Output:**
- CareEvent.cs model in Domain layer
- CareEventType.cs enum
- Foreign key relationship to Plant
- Entity configuration in DbContext

**Reference:** README.md Section 3.2 (Care Event Tracking) details the requirements for care events.

### Task 3.2: Implement CRUD endpoints for care events
**Purpose:** Create the complete set of API endpoints that allow users to record, retrieve, update and delete care activities, forming the backend backbone of the care tracking feature.

**Requirements:**
- Create CareEventController with complete CRUD actions
- Implement service methods for care event operations
- Create DTOs for care event operations
- Document API endpoints
- Handle validation and errors

**Technology:**
- ASP.NET Core Controllers
- Service pattern
- DTOs for data transfer

**Design Patterns:**
- Repository pattern
- Service pattern
- DTO pattern
- Command pattern for operations

**Output:**
- CareEventController with endpoints
- Service methods implementation
- DTOs for requests and responses
- Swagger documentation

**Reference:** README.md Section 3.2 outlines the care event tracking requirements.

### Task 3.3: Build CareEventForm component
**Purpose:** Provide users with an intuitive interface for recording different types of care activities, making it easy to maintain an accurate care history for each plant.

**Requirements:**
- Create form component for adding/editing care events
- Support different event types with appropriate fields
- Implement date/time selection
- Add validation for required fields
- Handle form submission

**Technology:**
- React components
- Form libraries
- Date/time picker components
- Validation

**Design Patterns:**
- Strategy pattern for different event types
- Composite pattern for form fields
- Observer pattern for form state
- Adapter pattern for API communication

**Output:**
- CareEventForm.tsx component
- Event type selection
- Dynamic fields based on event type
- Validation rules
- Submission handling

**Reference:** README.md Section 9.2 (Care Event Tracking) details the user needs for logging different types of care events.

### Task 3.4: Add CareEventList to plant detail page
**Purpose:** Display a plant's complete care history in a user-friendly format, allowing users to review past care activities and identify patterns in plant maintenance.

**Requirements:**
- Create component to display care events for a plant
- Implement chronological ordering
- Support filtering by event type
- Add pagination for many events
- Include event details and timestamps

**Technology:**
- React components
- List virtualization (optional)
- Filtering and sorting
- Responsive design

**Design Patterns:**
- Composite pattern for list structure
- Strategy pattern for sorting/filtering
- Virtual list pattern for performance
- Observer pattern for updates

**Output:**
- CareEventList.tsx component
- Filtering controls
- Chronological display
- Pagination controls
- Integration with plant detail page

**Reference:** README.md Section 3.2 mentions the need to view care history for each plant.

### Task 3.5: Add health status properties to Plant entity
**Purpose:** Enhance the plant data model to track plant health over time, enabling users to monitor health trends and prioritize plants that need attention.

**Requirements:**
- Add HealthStatus property to Plant entity
- Create observation history tracking
- Add timestamp for status changes
- Implement status change notes
- Update database schema

**Technology:**
- EF Core Entity updates
- Database migrations
- Enum for health states

**Design Patterns:**
- State pattern for health status
- Memento pattern for history
- Observer pattern for status changes

**Output:**
- Updated Plant.cs with health properties
- Observation.cs entity (if applicable)
- Migration for schema changes
- Updated repository methods

**Reference:** README.md Section 3.3 (Health Monitoring) details the health status tracking requirements.

### Task 3.6: Implement health update endpoints
**Purpose:** Create the API endpoints necessary for updating and tracking plant health status, allowing users to record health observations and view health history.

**Requirements:**
- Create endpoints for updating plant health status
- Implement service methods for health management
- Create DTOs for health update operations
- Document API endpoints
- Record health history

**Technology:**
- ASP.NET Core Controllers
- Service pattern
- DTOs for data transfer

**Design Patterns:**
- Command pattern for status updates
- Memento pattern for history
- Repository pattern
- Service pattern

**Output:**
- Health endpoints in PlantController or separate controller
- Service methods for health updates
- DTOs for health operations
- Swagger documentation

**Reference:** README.md Section 3.3 outlines the requirements for health status tracking.

### Task 3.7: Create health status update component
**Purpose:** Provide users with an intuitive interface for updating plant health status, making it easy to record observations and maintain an accurate health history.

**Requirements:**
- Build UI component for updating plant health
- Create visual indicators for different health states
- Add form for observations/notes
- Implement status change confirmation
- Provide health history view

**Technology:**
- React components
- Visual indicators (icons, colors)
- Form components
- Status visualization

**Design Patterns:**
- State pattern for health visualization
- Strategy pattern for different status types
- Observer pattern for status updates
- Command pattern for update actions

**Output:**
- HealthStatusUpdate.tsx component
- Visual indicators for health states
- Notes/observation input
- Status history display
- Integration with plant detail page

**Reference:** README.md Section 3.3 mentions updating plant health status with observations.

### Task 3.8: Add visual health indicators to plant cards
**Purpose:** Make plant health status immediately visible through visual cues, helping users quickly identify plants that need attention in list and grid views.

**Requirements:**
- Create visual indicators for plant health on list views
- Use color coding and/or icons for status
- Ensure accessible design (not color-only)
- Add tooltips for status explanation
- Implement consistent design language

**Technology:**
- React components
- CSS for visual styling
- Accessibility features
- Icon library

**Design Patterns:**
- Decorator pattern for status indicators
- Flyweight pattern for reusable status visuals
- Strategy pattern for different status visualizations

**Output:**
- Health indicator component
- CSS for status visualization
- Integration with PlantCard component
- Accessible design elements
- Consistent styling across application

**Reference:** README.md Section 3.3 mentions visual indicators for plants requiring attention.

### Task 3.9: Set up SignalR hub for plant events
**Purpose:** Establish the server-side infrastructure for real-time updates, enabling instant notification of plant changes across the application.

**Requirements:**
- Create SignalR hub for real-time plant updates
- Define methods for different event types
- Implement connection management
- Set up groups for targeted notifications
- Configure security and authentication

**Technology:**
- SignalR
- ASP.NET Core
- Authentication integration

**Design Patterns:**
- Observer pattern
- Publish/Subscribe pattern
- Facade pattern for client API

**Output:**
- PlantHub.cs SignalR hub
- Connection management
- Event notification methods
- Security configuration
- Integration with services

**Reference:** README.md Section 8.1 mentions SignalR as part of the architecture.

### Task 3.10: Implement client-side SignalR connection
**Purpose:** Create the frontend counterpart to the SignalR hub, allowing the UI to receive and respond to real-time updates from the server.

**Requirements:**
- Set up SignalR client in React application
- Implement connection management
- Add reconnection strategies
- Create event handlers for updates
- Manage connection state

**Technology:**
- SignalR client library
- React hooks for integration
- Connection state management

**Design Patterns:**
- Observer pattern
- Strategy pattern for different event types
- Factory pattern for message handlers

**Output:**
- SignalR client configuration
- Custom hook for SignalR connection
- Event handling methods
- Reconnection logic
- Connection state indicators

**Reference:** README.md Section 14.2.5 implies the need for real-time updates in the UI.

### Task 3.11: Add real-time updates for plant changes
**Purpose:** Enhance the user experience by reflecting plant changes instantly across the application, ensuring users always see the most current plant information without manual refresh.

**Requirements:**
- Trigger SignalR events on plant changes
- Update UI components when events received
- Implement optimistic UI updates
- Handle conflicts between local and server state
- Add visual feedback for updates

**Technology:**
- SignalR integration
- React state management
- UI animation for changes

**Design Patterns:**
- Observer pattern
- Command pattern
- Strategy pattern for update types
- Optimistic Update pattern

**Output:**
- Service methods triggering SignalR events
- Client handlers for plant update events
- State reconciliation logic
- Visual transition effects
- Toast notifications for changes

**Reference:** README.md Section 8.2 mentions real-time updates via SignalR to dashboard.

### Task 3.12: Add real-time updates for care events
**Purpose:** Provide immediate feedback when care events are recorded, creating a more responsive application experience and ensuring all users see the latest care activities.

**Requirements:**
- Trigger SignalR events when care events added/updated
- Update care event list in real-time
- Add visual indicators for new events
- Implement sound notifications (optional)
- Handle out-of-order events

**Technology:**
- SignalR integration
- React state management
- UI animation for changes
- Audio API (optional)

**Design Patterns:**
- Observer pattern
- Command pattern with real-time notification
- Strategy pattern for event types
- Queue pattern for event ordering

**Output:**
- Service methods triggering SignalR events
- Client handlers for care event updates
- Visual transition effects for new events
- Sound notification system (optional)
- Event reconciliation logic

**Reference:** README.md Section 9.2 implies the need for immediate feedback when logging care events.

## Epic 4: Visual Plant Journey
**Description:** Create functionality for photo uploads and visual growth timeline

### Task 4.1: Create photo storage service
**Purpose:** Establish a secure, efficient system for storing and retrieving plant photos, creating the foundation for visual plant tracking and growth documentation.

**Requirements:**
- Implement service for storing plant photos
- Support file validation and security checks
- Create thumbnail generation
- Handle file system or blob storage
- Implement path/URL generation

**Technology:**
- .NET File handling
- Image processing libraries
- Blob storage (optional)
- Secure file validation

**Design Patterns:**
- Strategy pattern for storage providers
- Facade pattern for simple interface
- Factory pattern for URL generation
- Chain of Responsibility for validation

**Output:**
- IPhotoStorageService interface
- FileSystemPhotoStorage implementation
- Image validation and processing
- Configuration for storage locations
- URL generation for stored images

**Reference:** README.md Section 3.4 (Growth Timeline) mentions the need to upload dated photos to document plant development.

### Task 4.2: Implement photo upload endpoint
**Purpose:** Create the API endpoint that enables users to upload plant photos, applying proper validation and security measures while associating images with the correct plants.

**Requirements:**
- Create endpoint for uploading plant photos
- Support multipart form data
- Implement validation for file types and sizes
- Connect to photo storage service
- Associate photos with plants

**Technology:**
- ASP.NET Core file upload
- Multipart form handling
- MIME type validation
- Storage service integration

**Design Patterns:**
- Strategy pattern for different upload types
- Chain of Responsibility for validation
- Facade pattern for upload workflow
- Command pattern for storage operations

**Output:**
- Upload endpoint in controller
- File validation logic
- Integration with storage service
- DTO for photo metadata
- Error handling for upload issues

**Reference:** README.md Section 3.4 mentions uploading photos to document plant development.

### Task 4.3: Build ImageUpload component
**Purpose:** Provide users with an intuitive, modern interface for adding photos to their plants, supporting various upload methods while giving visual feedback throughout the process.

**Requirements:**
- Create React component for image uploads
- Support drag-and-drop functionality
- Add file selection dialog
- Implement preview before upload
- Show progress indicator during upload

**Technology:**
- React components
- File API
- Drag-and-drop API
- Image preview capabilities
- Upload progress tracking

**Design Patterns:**
- Composite pattern for upload UI
- Observer pattern for upload progress
- Strategy pattern for upload methods
- Command pattern for upload process

**Output:**
- ImageUpload.tsx component
- Drag-and-drop area
- File selection button
- Image preview component
- Progress indicator
- Error display

**Reference:** README.md Section 14.2.4 mentions ImageUpload.tsx as a common component.

### Task 4.4: Add photo display in plant details
**Purpose:** Create a visually appealing way to display plant photos in the detail view, enhancing the user experience by making images a central part of plant information.

**Requirements:**
- Create component to display plant's primary photo
- Implement responsive image display
- Add lightbox for enlarged viewing
- Handle missing/loading images gracefully
- Support image actions (set as primary, delete)

**Technology:**
- React components
- Responsive image techniques
- Lightbox library (optional)
- Image optimization

**Design Patterns:**
- Facade pattern for image handling
- Proxy pattern for image loading
- Strategy pattern for image actions
- Observer pattern for image updates

**Output:**
- PlantPhoto.tsx component
- Responsive image display
- Lightbox integration
- Fallback for missing images
- Image action buttons

**Reference:** README.md Section 3.1 mentions uploading a primary photo for each plant.

### Task 4.5: Create GrowthPhoto entity and relationship to Plant
**Purpose:** Build the data model for tracking plant growth through multiple photos over time, establishing the foundation for the visual timeline feature that helps users document their plants' development.

**Requirements:**
- Design GrowthPhoto entity with necessary properties
- Establish relationship to Plant entity
- Add metadata fields (date, caption, tags)
- Support ordering by date
- Add validation rules

**Technology:**
- C# Entity classes
- EF Core relationships
- Data annotations
- Database indexing

**Design Patterns:**
- Rich Domain Model
- Entity-Relationship pattern
- Value Objects for complex properties
- Ordered Collection pattern

**Output:**
- GrowthPhoto.cs entity in Domain layer
- Relationship configuration in DbContext
- Database migration for new table
- Repository methods for photo management

**Reference:** README.md Section 6 includes GrowthPhotos in the database schema.

### Task 4.6: Implement timeline photo upload endpoints
**Purpose:** Create API endpoints that enable users to manage timeline photos with metadata, supporting the visual documentation of plant growth and changes over time.

**Requirements:**
- Create endpoints for timeline photo management
- Support adding photos with metadata
- Implement update and delete operations
- Optimize for multiple photo uploads
- Add filtering by date range

**Technology:**
- ASP.NET Core Controllers
- File upload handling
- Batch processing
- Query optimization

**Design Patterns:**
- Repository pattern
- Service pattern
- Command pattern for operations
- Specification pattern for filtering

**Output:**
- TimelineController with endpoints
- Service methods for timeline operations
- DTOs for timeline photos
- Batch upload support
- Filtering capabilities

**Reference:** README.md Section 3.4 outlines the growth timeline feature including photo uploads with dates.

### Task 4.7: Build TimelineView component
**Purpose:** Provide users with a visually engaging, chronological view of their plant's development over time, allowing them to see progress and compare growth stages.

**Requirements:**
- Create component for chronological photo display
- Implement date-based organization
- Add caption display and editing
- Support zooming and navigation
- Create comparison view for before/after

**Technology:**
- React components
- Timeline visualization libraries
- Date handling
- Image comparison tools

**Design Patterns:**
- Composite pattern for timeline structure
- Strategy pattern for different views
- Observer pattern for updates
- Command pattern for timeline actions

**Output:**
- TimelineView.tsx component
- Chronological photo display
- Caption editing interface
- Navigation controls
- Before/after comparison view

**Reference:** README.md Section 3.4 mentions viewing a chronological gallery for each plant.

### Task 4.8: Add photo gallery with chronological display
**Purpose:** Create a comprehensive view of all plant photos with filtering and sorting capabilities, giving users a complete visual history of their plants' development.

**Requirements:**
- Implement photo gallery for all plant photos
- Create chronological sorting and display
- Add filtering by date range
- Support batch operations on photos
- Implement masonry or grid layout

**Technology:**
- React components
- Gallery/grid libraries
- Responsive design
- Date filtering
- Batch operations UI

**Design Patterns:**
- Composite pattern for gallery structure
- Strategy pattern for different layouts
- Command pattern for batch operations
- Virtual scrolling pattern for performance

**Output:**
- GalleryView.tsx component
- Chronological sorting controls
- Date range filters
- Batch selection interface
- Responsive grid/masonry layout

**Reference:** README.md Section 3.4 mentions the need to compare changes over time in the growth timeline.

## Epic 5: Care Planning
**Description:** Implement reminders system and care scheduling features

### Task 5.1: Create endpoints for dashboard data
**Purpose:** Build the API foundation for the dashboard feature, aggregating relevant plant data to provide users with a quick overview of their collection status and care priorities.

**Requirements:**
- Implement endpoint to retrieve dashboard overview data
- Include plants needing attention grouped by status
- Add upcoming care events/reminders
- Create recently added plants section
- Optimize query performance

**Technology:**
- ASP.NET Core Controllers
- LINQ for data aggregation
- Query optimization
- DTOs for dashboard data

**Design Patterns:**
- Repository pattern
- Facade pattern for dashboard services
- Specification pattern for queries
- DTO pattern for data transfer

**Output:**
- DashboardController with endpoints
- Service methods for dashboard data
- DTOs for dashboard sections
- Optimized queries for performance

**Reference:** README.md Section 9.6 (Dashboard Overview) details the requirements for the main dashboard.

### Task 5.2: Build Dashboard component
**Purpose:** Create the central hub of the application where users can get an at-a-glance view of their plant collection status, upcoming tasks, and plants requiring attention.

**Requirements:**
- Create main dashboard component with sections
- Implement responsive layout for different devices
- Add navigation to detailed views
- Create loading states for dashboard sections
- Implement error handling for section loading

**Technology:**
- React components
- Responsive grid layout
- Card components
- Dashboard widgets

**Design Patterns:**
- Composite pattern for dashboard structure
- Strategy pattern for different sections
- Skeleton UI pattern for loading
- Error boundary pattern

**Output:**
- Dashboard.tsx component
- Section components for different data
- Responsive layout
- Navigation to detail views
- Loading states and error handling

**Reference:** README.md Section 9.6 describes the dashboard that prioritizes information for users.

### Task 5.3: Add "needs attention" section
**Purpose:** Help users quickly identify and prioritize plants that require care or intervention, drawing attention to plants with health issues or overdue care tasks.

**Requirements:**
- Create component for plants needing attention
- Group plants by health status
- Implement priority sorting
- Add quick action buttons
- Create visual indicators for urgency

**Technology:**
- React components
- Grouping and sorting logic
- Visual status indicators
- Action buttons

**Design Patterns:**
- Composite pattern for section structure
- Strategy pattern for different status groups
- Command pattern for quick actions
- Observer pattern for status updates

**Output:**
- NeedsAttentionSection.tsx component
- Grouped display of plants by status
- Priority indicators
- Quick action buttons
- Integration with dashboard

**Reference:** README.md Section 9.6 mentions plants requiring immediate attention as part of the dashboard.

### Task 5.4: Implement recently added plants section
**Purpose:** Make newly added plants easily accessible from the dashboard, helping users track and complete the setup of plants they've recently started monitoring.

**Requirements:**
- Create component for recently added plants
- Display plants in chronological order
- Include key plant information
- Add quick navigation to plant details
- Limit to reasonable number of recent plants

**Technology:**
- React components
- Date sorting
- Card components
- Navigation integration

**Design Patterns:**
- Composite pattern for section structure
- Observer pattern for new plant updates
- Factory pattern for plant card creation
- Strategy pattern for sorting

**Output:**
- RecentlyAddedSection.tsx component
- Chronological display of recent plants
- Condensed plant information cards
- Navigation to plant details
- Configuration for number of plants shown

**Reference:** README.md Section 9.6 mentions recently added plants as part of the dashboard.

### Task 5.5: Create Reminder entity with relationship to Plant
**Purpose:** Establish the data model for plant care reminders, enabling the scheduling and tracking of recurring maintenance tasks customized for each plant's needs.

**Requirements:**
- Design Reminder entity with all necessary properties
- Create ReminderType enum for categorization
- Establish relationship to Plant entity
- Add scheduling and recurrence properties
- Implement completion tracking

**Technology:**
- C# Entity classes
- EF Core relationships
- Date/time handling
- Recurrence patterns

**Design Patterns:**
- Rich Domain Model
- Entity-Relationship pattern
- Value Objects for complex properties (recurrence)
- State pattern for reminder status

**Output:**
- Reminder.cs entity in Domain layer
- ReminderType.cs enum
- Relationship configuration in DbContext
- Database migration for new table
- Repository methods for reminders

**Reference:** README.md Section 6 includes CareReminders in the database schema with relationships to Plants.

### Task 5.6: Implement CRUD endpoints for reminders
**Purpose:** Create the API endpoints for managing care reminders, giving users complete control over scheduling, modifying, and completing care tasks for their plants.

**Requirements:**
- Create ReminderController with CRUD operations
- Implement service methods for reminder management
- Create DTOs for reminder operations
- Add endpoints for filtering reminders
- Support recurring reminder generation

**Technology:**
- ASP.NET Core Controllers
- Service pattern
- Date/time calculations
- Recurrence pattern implementation

**Design Patterns:**
- Repository pattern
- Service pattern
- Specification pattern for queries
- Strategy pattern for recurrence types

**Output:**
- ReminderController with endpoints
- Service methods for reminder operations
- DTOs for reminders
- Filtering endpoints
- Recurrence handling logic

**Reference:** README.md Section 3.5 (Care Reminders) outlines the reminder system requirements.

### Task 5.7: Build RemindersView component
**Purpose:** Provide users with a clear overview of upcoming and overdue care tasks, making it easy to manage plant maintenance and track completion status.

**Requirements:**
- Create component for viewing and managing reminders
- Group reminders by due date and type
- Implement status indicators (due, overdue)
- Add mark as complete functionality
- Support filtering and sorting

**Technology:**
- React components
- Date grouping and formatting
- Status visualization
- Filter controls

**Design Patterns:**
- Composite pattern for reminder structure
- Strategy pattern for different views
- Command pattern for reminder actions
- Observer pattern for updates

**Output:**
- RemindersView.tsx component
- Date-based grouping of reminders
- Status indicators
- Completion controls
- Filter/sort functionality

**Reference:** README.md Section 9.3 (Smart Care Reminders) describes the reminder functionality from a user perspective.

### Task 5.8: Add reminder creation interface
**Purpose:** Allow users to create custom care reminders with flexible recurrence patterns, helping them establish personalized care routines for their plants.

**Requirements:**
- Build form for creating and editing reminders
- Support different reminder types
- Implement recurrence pattern selection
- Add date/time picker
- Include validation for reminder parameters

**Technology:**
- React form components
- Date/time picker
- Recurrence pattern UI
- Form validation

**Design Patterns:**
- Strategy pattern for reminder types
- Builder pattern for recurrence rules
- Composite pattern for form structure
- Validator pattern for form validation

**Output:**
- ReminderForm.tsx component
- Type selection controls
- Recurrence pattern interface
- Date/time selection
- Validation rules

**Reference:** README.md Section 9.3 mentions generating personalized care schedules based on plant requirements.

### Task 5.9: Create calendar data endpoints
**Purpose:** Build the API endpoints needed for calendar visualization, aggregating care events and reminders in a format optimized for timeline display.

**Requirements:**
- Implement endpoints for calendar view data
- Support date range parameters
- Return care events and reminders
- Add filtering by plant and event type
- Optimize query performance

**Technology:**
- ASP.NET Core Controllers
- Date range handling
- LINQ for data querying
- DTO design for calendar data

**Design Patterns:**
- Repository pattern
- Specification pattern for queries
- Adapter pattern for calendar formatting
- Facade pattern for calendar service

**Output:**
- CalendarController with endpoints
- Service methods for calendar data
- DTOs for calendar events
- Date range filtering
- Query optimization

**Reference:** README.md Section 9.3 implies the need for calendar view data for care tasks.

### Task 5.10: Build CalendarView component
**Purpose:** Give users a familiar calendar interface to visualize upcoming care tasks, helping them plan and manage plant care activities within their schedule.

**Requirements:**
- Create calendar component for care tasks
- Implement month, week, and day views
- Add event visualization on calendar
- Support navigation between time periods
- Enable clicking events for details

**Technology:**
- React calendar components
- Date manipulation libraries
- Event visualization
- Responsive design

**Design Patterns:**
- Strategy pattern for different views
- Composite pattern for calendar structure
- Observer pattern for date selection
- Factory pattern for event rendering

**Output:**
- CalendarView.tsx component
- Month/week/day view options
- Event visualization on dates
- Navigation controls
- Event detail interaction

**Reference:** README.md Section 5.2 implies the need for a care calendar as part of the care planning feature.

### Task 5.11: Implement month/week/day views
**Purpose:** Provide different time-scale perspectives on care tasks, allowing users to zoom in on daily activities or get a broader view of their care schedule.

**Requirements:**
- Create different time period views for calendar
- Design consistent UI across views
- Support switching between views
- Optimize performance for dense event periods
- Ensure responsive design for all views

**Technology:**
- React components
- Date calculation libraries
- Responsive layout techniques
- Performance optimization

**Design Patterns:**
- Strategy pattern for different views
- State pattern for view selection
- Flyweight pattern for event rendering
- Virtual scrolling for performance

**Output:**
- MonthView.tsx component
- WeekView.tsx component
- DayView.tsx component
- View switching controls
- Consistent event rendering

**Reference:** README.md Section 9.3 implies different time period views for planning care activities.

### Task 5.12: Add event indicators for different care types
**Purpose:** Create a visual language that makes different types of care activities instantly recognizable in the calendar, helping users quickly understand their care schedule.

**Requirements:**
- Create visual indicators for different care event types
- Implement color coding and/or icons
- Add tooltip information for events
- Ensure accessibility (not color-only)
- Support density visualization for multiple events

**Technology:**
- React components
- CSS for styling
- Icon system
- Tooltip components
- Accessibility features

**Design Patterns:**
- Strategy pattern for different event types
- Decorator pattern for visual indicators
- Flyweight pattern for reusable indicators
- Factory pattern for indicator creation

**Output:**
- EventIndicator.tsx component
- Color/icon system for event types
- Tooltip implementation
- Density handling for multiple events
- Accessible design

**Reference:** README.md Section 9.3 mentions visual indicators for different care activities.

### Task 5.13: Implement reminder generation service
**Purpose:** Build an intelligent system that automatically suggests appropriate care schedules based on plant species, conditions, and seasonal factors, reducing the manual work of reminder creation.

**Requirements:**
- Create service for automatic reminder generation
- Base reminders on plant care requirements
- Implement frequency calculation algorithms
- Support seasonal adjustments
- Handle special care requirements

**Technology:**
- C# services
- Algorithm design
- Date calculation
- Configuration system

**Design Patterns:**
- Strategy pattern for different reminder types
- Factory pattern for reminder creation
- Template method for generation steps
- Chain of Responsibility for adjustments

**Output:**
- ReminderGenerationService implementation
- Algorithms for frequency calculation
- Seasonal adjustment logic
- Special case handling
- Configuration options

**Reference:** README.md Section 9.3 describes smart care reminders based on plant-specific requirements.

### Task 5.14: Create endpoints for suggested reminders
**Purpose:** Enable the application to provide smart care recommendations to users, with API endpoints that deliver personalized suggestions based on plant data and care patterns.

**Requirements:**
- Implement API endpoints for suggested reminders
- Return recommendations based on plant data
- Support accepting/modifying suggestions
- Include explanation for suggestions
- Allow batch operations on suggestions

**Technology:**
- ASP.NET Core Controllers
- Suggestion algorithm integration
- DTOs for suggestions
- Batch processing

**Design Patterns:**
- Specification pattern for suggestion rules
- Command pattern for acceptance
- Strategy pattern for different suggestion types
- Batch command pattern

**Output:**
- SuggestionsController with endpoints
- Service methods for generating suggestions
- DTOs for suggestion data
- Batch acceptance endpoint
- Explanation generation

**Reference:** README.md Section 9.3 implies automated reminder generation based on plant needs.

### Task 5.15: Add suggestion acceptance UI
**Purpose:** Provide users with an interface to review and act on care suggestions, giving them control over which automated recommendations to incorporate into their care schedule.

**Requirements:**
- Create interface for reviewing suggested reminders
- Support accepting, modifying, or rejecting suggestions
- Implement batch actions on multiple suggestions
- Add explanation display for suggestion reasoning
- Provide feedback on acceptance

**Technology:**
- React components
- Batch selection UI
- Form components for modification
- Toast notifications

**Design Patterns:**
- Command pattern for suggestion actions
- Strategy pattern for different suggestion types
- Observer pattern for batch selection
- Composite pattern for suggestion groups

**Output:**
- SuggestionReview.tsx component
- Accept/modify/reject controls
- Batch selection interface
- Explanation display
- Feedback notifications

**Reference:** README.md Section 9.3 mentions adjusting frequencies based on completion patterns.

### Task 5.16: Implement recurring reminder pattern
**Purpose:** Create a flexible, powerful system for generating recurring reminders that adapts to different plant needs and seasonal changes, ensuring timely care notifications.

**Requirements:**
- Create system for recurring reminder generation
- Support different recurrence patterns (daily, weekly, monthly)
- Implement exception handling for skipped occurrences
- Add end date or occurrence count options
- Support editing recurrence patterns

**Technology:**
- Recurrence rule implementation
- Date calculation algorithms
- Pattern storage and retrieval
- Rule serialization

**Design Patterns:**
- Specification pattern for recurrence rules
- Interpreter pattern for rule parsing
- Factory pattern for rule creation
- Iterator pattern for occurrence generation

**Output:**
- RecurrencePattern class/structure
- Rule parsing and generation algorithms
- Storage in database
- Editing interface
- Occurrence calculation methods

**Reference:** README.md Section 9.7 (Seasonal Adjustments) implies the need for recurring patterns that adjust with seasons.

## Epic 6: User Experience Enhancement
**Description:** Enhance the user experience and address feedback

### Task 6.1: Optimize database queries
**Purpose:** Improve application performance and responsiveness by reducing database load, optimizing queries, and implementing smart caching strategies for frequently accessed data.

**Requirements:**
- Analyze and optimize slow database queries
- Implement proper indexing for frequent queries
- Add caching for repetitive queries
- Use eager loading for related entities
- Monitor and benchmark query performance

**Technology:**
- Entity Framework query optimization
- SQL Server indexing
- Caching mechanisms
- Performance monitoring tools

**Design Patterns:**
- Repository pattern optimizations
- Specification pattern for queries
- Caching patterns
- Lazy/Eager loading strategies

**Output:**
- Optimized queries in repositories
- Database index configurations
- Caching implementation
- Performance metrics
- Documentation of optimizations

**Reference:** README.md Section 7 (Non-Functional Requirements) mentions performance requirements with page load times under 2 seconds.

### Task 6.2: Implement pagination for large collections
**Purpose:** Enhance application performance and usability when dealing with large plant collections by loading data in manageable chunks, improving load times and reducing resource usage.

**Requirements:**
- Add pagination to all list views
- Implement server-side pagination in API
- Create UI controls for page navigation
- Support configurable page sizes
- Maintain state during pagination

**Technology:**
- API pagination parameters
- LINQ Skip/Take
- React pagination components
- URL query parameters

**Design Patterns:**
- Page Object pattern
- Iterator pattern
- Repository pagination pattern
- State management patterns

**Output:**
- Pagination parameters in DTOs
- Repository methods with pagination
- PaginationControls.tsx component
- State persistence during navigation
- Configurable page size options

**Reference:** README.md Section 7 mentions scalability to support collections of 100+ plants.

### Task 6.3: Add caching for frequently accessed data
**Purpose:** Reduce database load and improve application responsiveness by intelligently caching frequently accessed data, ensuring a smooth user experience even with large plant collections.

**Requirements:**
- Implement caching strategy for frequently accessed data
- Add cache invalidation on updates
- Use memory and distributed caching as appropriate
- Configure cache duration policies
- Monitor cache hit rates

**Technology:**
- .NET Memory Cache
- Distributed cache options
- Cache invalidation strategies
- Monitoring tools

**Design Patterns:**
- Cache-Aside pattern
- Decorator pattern for caching
- Publisher-Subscriber for invalidation
- Strategy pattern for cache policies

**Output:**
- CachingService implementation
- Cache configuration
- Invalidation triggers in services
- Cache policy configuration
- Monitoring of cache effectiveness

**Reference:** README.md Section 7 mentions performance requirements that would benefit from caching.

### Task 6.4: Optimize image loading and display
**Purpose:** Improve the performance and user experience of photo features by implementing smart image loading techniques that reduce bandwidth usage and improve perceived speed.

**Requirements:**
- Implement lazy loading for images
- Create responsive images with srcset
- Add image compression for uploads
- Implement loading placeholders
- Add progressive image loading

**Technology:**
- Lazy loading libraries
- Image compression tools
- Responsive image techniques
- CSS for placeholders

**Design Patterns:**
- Lazy Loading pattern
- Proxy pattern for images
- Builder pattern for image URLs
- Placeholder pattern

**Output:**
- LazyImage.tsx component
- Image compression service
- Responsive image configuration
- Loading placeholders
- Progressive loading implementation

**Reference:** README.md Section 7 mentions image optimization as part of performance requirements.

### Task 6.5: Create welcome experience
**Purpose:** Provide new users with a guided introduction to Plant Buddy, helping them understand key features and get started quickly with their plant care management.

**Requirements:**
- Design onboarding flow for new users
- Create welcome screen with key features
- Implement guided tour functionality
- Add getting started resources
- Support dismissing/skipping onboarding

**Technology:**
- React components
- Tour/guide libraries
- Local storage for preferences
- Animation libraries

**Design Patterns:**
- State pattern for tour steps
- Strategy pattern for different user types
- Observer pattern for tour events
- Memento pattern for progress saving

**Output:**
- WelcomeScreen.tsx component
- GuidedTour.tsx component
- Getting started resources
- Preference storage
- Skip/dismiss functionality

**Reference:** README.md Section 7 mentions usability for non-technical users, which would benefit from a welcome experience.

### Task 6.6: Add contextual help tooltips
**Purpose:** Enhance user understanding of the application by providing context-sensitive help and explanations throughout the interface, improving usability for users of all experience levels.

**Requirements:**
- Implement contextual help throughout the application
- Create tooltip components with helpful information
- Add help icons next to complex features
- Ensure tooltips are accessible
- Support keyboard navigation for help

**Technology:**
- React tooltip components
- Accessibility features
- Help content management
- Icon system

**Design Patterns:**
- Decorator pattern for help tooltips
- Flyweight pattern for reusable tooltips
- Strategy pattern for different help types
- Factory pattern for tooltip creation

**Output:**
- HelpTooltip.tsx component
- Help content system
- Icon integration
- Accessibility implementation
- Keyboard navigation support

**Reference:** README.md Section 6.3 (User Onboarding) mentions contextual help tooltips.

### Task 6.7: Implement empty state guidance
**Purpose:** Provide helpful guidance and suggested actions when users encounter empty views, turning potentially confusing moments into opportunities for engagement and learning.

**Requirements:**
- Create helpful empty states for lists and views
- Add suggested actions for empty states
- Implement illustrations or icons for visual appeal
- Ensure empty states are informative
- Provide direct links to relevant actions

**Technology:**
- React components
- SVG illustrations
- Action button components
- Conditional rendering

**Design Patterns:**
- State pattern for empty conditions
- Strategy pattern for different empty states
- Template method for consistent structure
- Command pattern for suggested actions

**Output:**
- EmptyState.tsx component
- Illustrations for different scenarios
- Action buttons for guided next steps
- Context-specific empty states
- Integration with all list views

**Reference:** README.md Section 6.3 mentions empty state guidance as part of user onboarding.

### Task 6.8: Create quick reference help section
**Purpose:** Offer users a centralized resource for finding answers to common questions and learning about application features, supporting self-service problem-solving and feature discovery.

**Requirements:**
- Design help and documentation section
- Create searchable FAQ
- Add quick tips for common tasks
- Implement keyboard shortcut reference
- Ensure help is accessible from anywhere in the app

**Technology:**
- React components
- Search functionality
- Keyboard shortcut library
- Modal components

**Design Patterns:**
- Composite pattern for help structure
- Strategy pattern for different help types
- Observer pattern for context-aware help
- Facade pattern for help service

**Output:**
- HelpSection.tsx component
- SearchableHelp.tsx component
- QuickTips.tsx component
- KeyboardShortcuts.tsx component
- Global help access button

**Reference:** README.md Section 6.3 mentions creating a quick reference help section for finding answers to common questions.

### Task 6.9: Implement basic LINQ query monitoring
**Purpose:** Create a simple monitoring system to identify and resolve performance issues with LINQ queries, ensuring the application remains responsive as data volume grows.

**Requirements:**
- Add basic logging for slow-running LINQ queries
- Implement query tags to identify problematic queries in logs
- Create simple performance reporting for database queries
- Set up alerting for queries exceeding performance thresholds
- Document common LINQ performance issues and solutions

**Technology:**
- Entity Framework Core diagnostics
- Logging infrastructure
- Query tagging

**Design Patterns:**
- Decorator pattern for logging
- Observer pattern for performance events

**Output:**
- Query logging configuration
- Performance threshold setup
- Documentation of common LINQ performance patterns and anti-patterns
- Simple dashboard for query performance metrics

**Reference:** PLANT_BUDDY_PRD.md Section 5.6.6 describes basic LINQ performance tips.

Each epic represents a coherent feature set that delivers complete user value, with tasks organized to build functionality incrementally across the full stack. 