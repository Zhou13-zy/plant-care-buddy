# Plant Buddy: Product Requirements Document

## 1. Product Overview

**Plant Buddy** is a digital plant care management system designed to help users track and maintain their houseplant collection. It enables plant enthusiasts to monitor plant health, schedule care activities, document growth with photos, and receive timely care reminders.

### Target Users
- Home gardeners with indoor plants
- Houseplant collectors
- Plant enthusiasts of varying expertise levels

### Core Concepts
| Term | Definition |
|------|------------|
| Plant | A tracked houseplant with species information, care requirements, and health history |
| Care Event | A recorded maintenance action (watering, fertilizing, repotting, etc.) |
| Growth Timeline | Chronological record of plant development with photos |
| Care Reminder | Scheduled notification for upcoming plant maintenance |

## 2. User Requirements

### 2.1 Plant Management
- **Add Plants**: Record new plants with name, species, acquisition date, location
- **View Collection**: Browse and search the complete plant collection
- **Plant Details**: Access comprehensive information about each plant
- **Edit Plants**: Update plant information as needed
- **Delete Plants**: Remove plants from the collection
- **Search/Filter**: Find plants by name, species, location, or health status

### 2.2 Care Tracking
- **Record Care Events**: Log watering, fertilizing, repotting, and other activities
- **View Care History**: See chronological history of care for each plant
- **Care Statistics**: View data on care frequency and patterns
- **Notes**: Add observations or special instructions for care events

### 2.3 Health Monitoring
- **Health Status**: Record and update plant health (Healthy, Needs Attention, Struggling)
- **Observations**: Document specific issues, growth patterns, or changes
- **Treatment Tracking**: Record pest/disease treatments and their effectiveness
- **Health History**: View the progression of plant health over time

### 2.4 Visual Growth Tracking
- **Photo Upload**: Add dated photos to document plant development
- **Timeline View**: Display chronological gallery of plant growth
- **Photo Captions**: Add notes to explain changes or highlight features
- **Before/After Comparison**: Compare plant appearance over time

### 2.5 Care Planning
- **Care Reminders**: Receive notifications for upcoming care tasks
- **Customized Schedules**: Set plant-specific care frequencies
- **Calendar View**: View upcoming care tasks in calendar format
- **Completion Tracking**: Mark reminders as completed
- **Seasonal Adjustments**: Modify care schedules based on growing season

### 2.6 Dashboard
- **Overview**: See at-a-glance summary of collection status
- **Priority Plants**: Quickly identify plants needing attention
- **Upcoming Tasks**: View imminent care activities
- **Recent Additions**: See recently added plants

## 3. Technical Requirements

### 3.1 Performance
- Page load times under 2 seconds
- Support for collections of 100+ plants
- Responsive design for all screen sizes
- Efficient image loading and optimization

### 3.2 Security
- Secure storage of user data
- Safe image handling and storage
- Protection against common web vulnerabilities

### 3.3 Reliability
- Data persistence with backup strategies
- Graceful error handling
- Connection recovery for real-time features

### 3.4 Compatibility
- Support for modern browsers (Chrome, Firefox, Safari, Edge)
- Desktop and mobile responsive design

## 4. Feature Specifications

### 4.1 Plant Management Module

#### 4.1.1 Plant Entity
**Properties:**
- ID (unique identifier)
- Name (required)
- Species (required)
- Acquisition Date
- Location
- Care Requirements (water frequency, light needs, etc.)
- Health Status (enum: Healthy, Needs Attention, Unhealthy, Dormant)
- Notes (additional information)
- Primary Photo URL

#### 4.1.2 Plant List View
**Functionality:**
- Display all plants in grid/list format
- Show plant name, species, photo, and health status
- Support sorting by various criteria
- Implement filtering by location, health status
- Add search capability by name or species

#### 4.1.3 Plant Detail View
**Functionality:**
- Display comprehensive plant information
- Show primary photo prominently
- List all care requirements
- Display current health status with visual indicator
- Provide access to care history, timeline, and reminders
- Include edit and delete options

### 4.2 Care Tracking Module

#### 4.2.1 Care Event Entity
**Properties:**
- ID (unique identifier)
- Plant ID (foreign key)
- Event Type (enum: Watering, Fertilizing, Repotting, Pruning, etc.)
- Date and Time
- Notes
- Photos (optional)

#### 4.2.2 Care Event Recording
**Functionality:**
- Form to add new care events
- Event type selection with appropriate fields
- Date/time picker
- Notes field
- Photo upload option
- Association with specific plant

#### 4.2.3 Care History View
**Functionality:**
- Chronological list of care events for a plant
- Filtering by event type
- Date range selection
- Visual indicators for different event types
- Statistics on care frequency

### 4.3 Health Tracking Module

#### 4.3.1 Health Status Updates
**Functionality:**
- Update health status with visual selection
- Add observations with timestamps
- Record treatments for issues
- Track resolution of problems

#### 4.3.2 Health History
**Functionality:**
- Timeline of health status changes
- Correlation with care events
- Filtering and date range selection
- Trend visualization

### 4.4 Visual Growth Module

#### 4.4.1 Growth Photo Entity
**Properties:**
- ID (unique identifier)
- Plant ID (foreign key)
- Image Path
- Capture Date
- Caption
- Is Primary (boolean)

#### 4.4.2 Photo Upload
**Functionality:**
- Multi-photo upload capability
- Date selection for each photo
- Caption/notes field
- Option to set as primary photo
- Progress indicator during upload

#### 4.4.3 Timeline View
**Functionality:**
- Chronological display of photos
- Grid or timeline format option
- Caption display
- Date filtering
- Zooming and navigation controls
- Before/after comparison view

### 4.5 Care Planning Module

#### 4.5.1 Reminder Entity
**Properties:**
- ID (unique identifier)
- Plant ID (foreign key)
- Reminder Type (enum: Watering, Fertilizing, etc.)
- Due Date
- Recurrence Pattern
- Is Completed (boolean)
- Completion Date (if applicable)

#### 4.5.2 Reminder Creation
**Functionality:**
- Manual reminder creation
- Automatic generation based on care requirements
- Recurrence pattern selection
- Custom and plant-specific frequencies

#### 4.5.3 Calendar View
**Functionality:**
- Month, week, and day view options
- Visual indicators for different reminder types
- Click to complete functionality
- Navigation between time periods

### 4.6 Dashboard Module

**Functionality:**
- Plants needing attention grouped by status
- Upcoming care reminders (next 7 days)
- Recently added plants
- Quick action buttons for common tasks
- Summary statistics (total plants, health distribution)

## 5. Technical Architecture

### 5.1 Frontend
- React 18 with TypeScript
- React Router for navigation
- Context API for state management
- Axios for API communication
- CSS/SCSS for styling
- SignalR for real-time updates

### 5.2 Backend
- ASP.NET Core 8 Web API
- Entity Framework Core 8 with LINQ for data access and query optimization
- LINQ as a core architectural component for:
  - Type-safe database queries with LINQ-to-Entities
  - In-memory data transformations with LINQ-to-Objects
  - Dynamic query construction with expression trees
  - Efficient data projections using Select transformations
  - Complex aggregate operations (GroupBy, Join, etc.)
- SQL Server database with optimized indexing for LINQ queries
- SignalR for real-time communication
- Repository pattern with LINQ specification pattern
- Service layer with encapsulated LINQ business logic
- Query pipeline patterns for complex data operations
- Compiled LINQ queries for performance-critical operations

### 5.3 Database
- Relational database with the following main tables:
  - Plants
  - CareEvents
  - GrowthPhotos
  - CareReminders
  - Observations
- Entity relationships as specified in data model

### 5.4 Deployment
- CI/CD pipeline for automated deployment
- Development, staging, and production environments
- Containerization for consistent environments

### 5.5 Design Patterns and OOP Principles

#### 5.5.1 Core OOP Principles
- **Encapsulation**: Hide implementation details inside classes while exposing clean interfaces
  - *Example*: Plant entity encapsulates its state and provides controlled access via properties
  - *Implementation*: Use of private fields with public properties, DTOs for data transfer

- **Inheritance**: Derive specialized types from more general ones
  - *Example*: Specialized reminder types extending from a base Reminder class
  - *Implementation*: Base classes for shared behavior, derived classes for specialization

- **Polymorphism**: Allow objects of different types to be treated through common interfaces
  - *Example*: Different care event types processed through a common interface
  - *Implementation*: Abstract base classes, interfaces, method overriding

- **Abstraction**: Focus on what objects do rather than how they do it
  - *Example*: Repository interfaces that abstract data access details
  - *Implementation*: Interface-based design, dependency injection

#### 5.5.2 Architectural Patterns

- **Repository Pattern**
  - *Purpose*: Abstract data access logic
  - *Application*: All entity data access (Plants, CareEvents, etc.)
  - *Benefits*: Separation of concerns, testability, consistent data access interface

- **Unit of Work Pattern**
  - *Purpose*: Maintain a list of objects affected by a business transaction
  - *Application*: Coordinated updates across multiple repositories
  - *Benefits*: Transaction integrity, reduced database calls

- **Service Layer Pattern**
  - *Purpose*: Define application's boundary and its set of operations
  - *Application*: PlantService, CareEventService, ReminderService, etc.
  - *Benefits*: Business logic encapsulation, separation from presentation layer

- **DTO Pattern**
  - *Purpose*: Transfer data between processes
  - *Application*: API request/response models, frontend-to-backend communication
  - *Benefits*: Decoupling, controlled data exposure, versioning

#### 5.5.3 Creational Patterns

- **Factory Method Pattern**
  - *Purpose*: Create objects without specifying exact class
  - *Application*: Reminder generation, specialized care event creation
  - *Benefits*: Encapsulated object creation, extensibility

- **Builder Pattern**
  - *Purpose*: Construct complex objects step by step
  - *Application*: Complex query building, recurrence pattern creation
  - *Benefits*: Construction separation from representation, reuse of construction code

#### 5.5.4 Structural Patterns

- **Adapter Pattern**
  - *Purpose*: Allow incompatible interfaces to work together
  - *Application*: Converting between domain models and DTOs
  - *Benefits*: Reuse existing code, cleanly separate domain from presentation

- **Decorator Pattern**
  - *Purpose*: Add responsibilities to objects dynamically
  - *Application*: Adding caching or logging to services
  - *Benefits*: Extend functionality without modifying original code

- **Composite Pattern**
  - *Purpose*: Compose objects into tree structures
  - *Application*: UI component hierarchy, dashboard sections
  - *Benefits*: Uniform treatment of individual and composite objects

#### 5.5.5 Behavioral Patterns

- **Observer Pattern**
  - *Purpose*: Define a dependency between objects
  - *Application*: Real-time updates via SignalR, UI state management
  - *Benefits*: Loose coupling, event-driven architecture

- **Strategy Pattern**
  - *Purpose*: Define a family of interchangeable algorithms
  - *Application*: Different reminder generation strategies, plant care calculations
  - *Benefits*: Runtime algorithm selection, encapsulated algorithm implementation

- **Command Pattern**
  - *Purpose*: Encapsulate a request as an object
  - *Application*: User actions (adding plant, recording care), undo functionality
  - *Benefits*: Parameterize clients with operations, support undoable operations

- **State Pattern**
  - *Purpose*: Allow an object to alter behavior when state changes
  - *Application*: Plant health status transitions, reminder completion states
  - *Benefits*: Localize state-specific behavior, cleanly manage state transitions

#### 5.5.6 Frontend Patterns

- **Container/Presentational Pattern**
  - *Purpose*: Separate data loading from presentation
  - *Application*: React components structure
  - *Benefits*: Reusable components, separation of concerns

- **Context API Pattern**
  - *Purpose*: Share state across component tree
  - *Application*: User preferences, plant collection state
  - *Benefits*: Avoid prop drilling, centralized state management

- **Custom Hooks Pattern**
  - *Purpose*: Extract and reuse stateful logic
  - *Application*: Data fetching, form handling, recurring operations
  - *Benefits*: Code reuse, logic separation from components

#### 5.5.7 Domain-Driven Design Principles

- **Entities and Value Objects**
  - *Purpose*: Distinguish objects with identity from those defined by attributes
  - *Application*: Plants as entities, care requirements as value objects
  - *Benefits*: Focused domain model, appropriate equality semantics

- **Aggregates and Aggregate Roots**
  - *Purpose*: Cluster entities and value objects into cohesive units
  - *Application*: Plant as aggregate root for care events and photos
  - *Benefits*: Enforced invariants, transactional boundaries

- **Domain Services**
  - *Purpose*: Encapsulate domain operations not belonging to entities
  - *Application*: Care schedule calculation, health status analysis
  - *Benefits*: Clean domain model, proper responsibility allocation

### 5.6 LINQ Implementation Strategies

LINQ (Language Integrated Query) provides powerful query capabilities directly within C#. In Plant Buddy, LINQ can be leveraged throughout the application to create more maintainable, readable, and efficient code.

#### 5.6.1 Repository Layer LINQ Usage

- **Filtering Plant Collections**
  ```csharp
  // Find plants that need watering soon (next 3 days)
  var plantsNeedingWater = _context.Plants
      .Where(p => p.WateringSchedule.NextWateringDate <= DateTime.Now.AddDays(3))
      .OrderBy(p => p.WateringSchedule.NextWateringDate)
      .ToListAsync();
  ```

- **Aggregating Care Events**
  ```csharp
  // Get watering statistics for a plant
  var wateringStats = await _context.CareEvents
      .Where(ce => ce.PlantId == plantId && ce.EventType == CareEventType.Watering)
      .GroupBy(ce => ce.EventDate.Month)
      .Select(g => new { Month = g.Key, Count = g.Count() })
      .ToListAsync();
  ```

- **Projecting to DTOs**
  ```csharp
  // Project to PlantSummaryDto
  var plantSummaries = await _context.Plants
      .Select(p => new PlantSummaryDto
      {
          Id = p.Id,
          Name = p.Name,
          Species = p.Species,
          HealthStatus = p.HealthStatus,
          PhotoUrl = p.PrimaryImagePath,
          DaysSinceLastWatering = (DateTime.Now - p.CareEvents
              .Where(ce => ce.EventType == CareEventType.Watering)
              .OrderByDescending(ce => ce.EventDate)
              .FirstOrDefault().EventDate).Days
      })
      .ToListAsync();
  ```

#### 5.6.2 Service Layer LINQ Usage

- **Complex Business Logic**
  ```csharp
  // Find plants with declining health trends
  var decliningHealthPlants = plants
      .Where(p => p.HealthObservations.Count >= 3)
      .Where(p => {
          var recentObservations = p.HealthObservations
              .OrderByDescending(o => o.ObservationDate)
              .Take(3)
              .ToList();
          return recentObservations[0].HealthStatus > recentObservations[2].HealthStatus;
      })
      .ToList();
  ```

- **Data Transformation**
  ```csharp
  // Group plants by location for dashboard
  var plantsByLocation = plants
      .GroupBy(p => p.Location)
      .Select(g => new LocationGroupDto
      {
          Location = g.Key,
          PlantCount = g.Count(),
          HealthyCount = g.Count(p => p.HealthStatus == PlantHealthStatus.Healthy),
          NeedsAttentionCount = g.Count(p => p.HealthStatus == PlantHealthStatus.NeedsAttention)
      })
      .OrderByDescending(g => g.PlantCount)
      .ToList();
  ```

#### 5.6.3 Controller Layer LINQ Usage

- **Query Parameter Processing**
  ```csharp
  // Filter plants based on query parameters
  var query = _context.Plants.AsQueryable();
  
  if (!string.IsNullOrEmpty(searchTerm))
      query = query.Where(p => p.Name.Contains(searchTerm) || p.Species.Contains(searchTerm));
      
  if (healthStatus.HasValue)
      query = query.Where(p => p.HealthStatus == healthStatus.Value);
      
  if (!string.IsNullOrEmpty(location))
      query = query.Where(p => p.Location == location);
      
  var result = await query
      .OrderBy(sortBy ?? "Name")
      .Skip((page - 1) * pageSize)
      .Take(pageSize)
      .ToListAsync();
  ```

- **Optimized Response Generation**
  ```csharp
  // Generate dashboard data in a single query
  var dashboardData = new DashboardDto
  {
      PlantCount = await _context.Plants.CountAsync(),
      HealthySummary = await _context.Plants
          .GroupBy(p => p.HealthStatus)
          .Select(g => new { Status = g.Key, Count = g.Count() })
          .ToListAsync(),
      RecentlyAdded = await _context.Plants
          .OrderByDescending(p => p.CreatedDate)
          .Take(5)
          .Select(p => new PlantSummaryDto { Id = p.Id, Name = p.Name, PhotoUrl = p.PrimaryImagePath })
          .ToListAsync(),
      UpcomingCare = await _context.Plants
          .SelectMany(p => p.CareReminders.Where(r => !r.IsCompleted && r.DueDate <= DateTime.Now.AddDays(7)))
          .OrderBy(r => r.DueDate)
          .Take(10)
          .Select(r => new ReminderDto { PlantId = r.PlantId, PlantName = r.Plant.Name, Type = r.Type, DueDate = r.DueDate })
          .ToListAsync()
  };
  ```

#### 5.6.4 Advanced LINQ Techniques

- **Async LINQ with Entity Framework**
  ```csharp
  // Using async operations for better scalability
  var plants = await _context.Plants
      .Include(p => p.CareEvents.Where(ce => ce.EventDate >= DateTime.Now.AddMonths(-1)))
      .Where(p => p.HealthStatus != PlantHealthStatus.Healthy)
      .ToListAsync();
  ```

- **Custom Extension Methods**
  ```csharp
  // Create reusable query components
  public static class PlantQueryExtensions
  {
      public static IQueryable<Plant> NeedsWatering(this IQueryable<Plant> query, int daysThreshold = 3)
      {
          return query.Where(p => p.WateringSchedule.NextWateringDate <= DateTime.Now.AddDays(daysThreshold));
      }
      
      public static IQueryable<Plant> WithHealthStatus(this IQueryable<Plant> query, PlantHealthStatus status)
      {
          return query.Where(p => p.HealthStatus == status);
      }
  }
  
  // Usage
  var plantsToCheck = _context.Plants
      .NeedsWatering(2)
      .WithHealthStatus(PlantHealthStatus.NeedsAttention)
      .ToListAsync();
  ```

- **Dynamic LINQ for User-Driven Queries**
  ```csharp
  // Allow dynamic sorting and filtering based on user preferences
  public async Task<PagedResult<PlantDto>> GetPlantsAsync(PlantQueryParams queryParams)
  {
      var query = _context.Plants.AsQueryable();
      
      // Apply dynamic filters
      foreach (var filter in queryParams.Filters)
      {
          switch (filter.Field)
          {
              case "name":
                  query = query.Where(p => p.Name.Contains(filter.Value));
                  break;
              case "species":
                  query = query.Where(p => p.Species.Contains(filter.Value));
                  break;
              // Add more filters as needed
          }
      }
      
      // Apply dynamic sorting
      if (!string.IsNullOrEmpty(queryParams.SortBy))
      {
          query = queryParams.SortDirection == "desc" 
              ? query.OrderByDescending(p => EF.Property<object>(p, queryParams.SortBy))
              : query.OrderBy(p => EF.Property<object>(p, queryParams.SortBy));
      }
      
      // Get total count for pagination
      var totalCount = await query.CountAsync();
      
      // Apply pagination
      var items = await query
          .Skip((queryParams.Page - 1) * queryParams.PageSize)
          .Take(queryParams.PageSize)
          .Select(p => _mapper.Map<PlantDto>(p))
          .ToListAsync();
          
      return new PagedResult<PlantDto>
      {
          Items = items,
          TotalCount = totalCount,
          Page = queryParams.Page,
          PageSize = queryParams.PageSize
      };
  }
  ```

#### 5.6.5 Benefits of LINQ in Plant Buddy

- **Reduced Code Volume**: Complex data operations expressed concisely
- **Type Safety**: Compile-time checking prevents runtime errors
- **Query Composability**: Build complex queries from simpler components
- **Readability**: Declarative syntax that expresses intent clearly
- **Performance**: Efficient translation to SQL when used with Entity Framework
- **Maintainability**: Consistent query pattern across the application
- **Testability**: Easier to unit test than complex imperative code

#### 5.6.6 Basic LINQ Performance Tips

When implementing LINQ in Plant Buddy, the following simple practices will help maintain good performance:

- **Select only what you need**
  ```csharp
  // Good: Only retrieve the fields needed
  var names = await _context.Plants
      .Where(p => p.HealthStatus == PlantHealthStatus.NeedsAttention)
      .Select(p => new { p.Id, p.Name })
      .ToListAsync();
  ```

- **Use proper indexes**
  ```csharp
  // Add indexes for frequently queried fields
  modelBuilder.Entity<Plant>()
      .HasIndex(p => p.HealthStatus);
  ```

- **Implement paging for large result sets**
  ```csharp
  // Return data in pages rather than all at once
  var pagedPlants = await _context.Plants
      .Skip((pageNumber - 1) * pageSize)
      .Take(pageSize)
      .ToListAsync();
  ```

- **Avoid N+1 query problems with Include**
  ```csharp
  // Load related data in a single query
  var plants = await _context.Plants
      .Include(p => p.CareEvents)
      .Where(p => p.HealthStatus == PlantHealthStatus.NeedsAttention)
      .ToListAsync();
  ```

By following these basic LINQ practices, Plant Buddy will maintain good performance while keeping the code clean and maintainable.

## 6. User Experience Guidelines

### 6.1 Design Principles
- Clean, intuitive interface
- Focus on visual plant representation
- Accessibility for all users
- Mobile-first responsive design
- Consistent visual language

### 6.2 Common Interactions
- Tap/click for selection
- Swipe/scroll for navigation
- Drag and drop for photo upload
- Pull to refresh for content updates
- Double-tap/click for expanded view

### 6.3 Visual Design
- Plant-inspired color palette
- Clear typography for readability
- Consistent iconography for plant care
- Photo-centric layout
- Visual status indicators

## 7. Implementation Milestones

### Phase 1: Core Functionality
- Plant management (CRUD operations)
- Basic care event tracking
- Simple dashboard

### Phase 2: Enhanced Features
- Photo uploads and timeline
- Care reminders and calendar
- Health tracking improvements

### Phase 3: Advanced Features
- Reminder generation algorithms
- Statistical analysis of plant care
- Seasonal adjustment features
- Enhanced user experience

## 8. Success Metrics

### 8.1 User Engagement
- Active usage (daily/weekly active users)
- Number of plants tracked per user
- Care events recorded per plant
- Photo uploads per plant

### 8.2 User Satisfaction
- Successful completion of care tasks
- Plant health improvements over time
- Feature usage distribution

### 8.3 Technical Performance
- Page load times
- API response times
- Error rates
- Data consistency

## 9. Appendix

### 9.1 Glossary
- **Care Frequency**: The recommended interval between care events
- **Primary Photo**: The main image representing a plant in list views
- **Dormancy**: A natural period of slowed growth requiring adjusted care

### 9.2 References
- Plant care best practices
- User research findings
- Technical documentation

---

This PRD will guide the development of Plant Buddy, ensuring that the application meets user needs while maintaining technical quality and coherence across features. 