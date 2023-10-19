# Architecture Overview: RPSSL Game API

This document offers a brief overview of the architectural decisions made during the development of the RPSSL Game API. The goal is to provide clarity on the choices made and offer insight into the thought process behind them.

## 1. Clean Architecture

### Why it was chosen:
Clean Architecture emphasizes the separation of concerns, making the codebase scalable and maintainable. This project's layers include:

- **API Layer**: Exposes endpoints and handles HTTP-specific tasks.
- **Application Layer**: Handles use-case logic and orchestrates between the domain and external concerns.
- **Domain**: Contains the core business rules, entities, and logic.
- **Infrastructure**: Deals with external concerns, integrations, and services.
- **Persistence**: Manages data persistence and database-related operations.

Using these layers ensures that the core business logic is isolated from external changes, allowing for easy modifications, scalability, and testing.

## 2. Command Query Responsibility Segregation (CQRS)

### Why it was chosen:
While CQRS might initially seem extensive for such an application, it offers distinct advantages:

- **Clear Separation**: Commands (writes) and queries (reads) are handled differently, leading to a more organized and understandable codebase.
- **Scalability**: In the event of scaling this application to a granular microservices architecture, it serves as an excellent starting point. For instance, the game logic can be separated into "Play" and "Choice" services.
  
## 3. Data Store

### Why it was chosen:
In-memory thread-safe collections (concurrent dictionary and queue) serve as the primary datastore. Given the application's nature, there were a few reasons behind this choice:

- **Performance**: In-memory data structures offer quick access times.
- **Simplicity**: For the scope of this project, a full-fledged database might be an overkill. 
- **Multi-user Requirement**: The thread-safe nature of these collections ensures consistent data even with multiple concurrent users.

**Note**: Data history is not persistent. From a user's perspective, only the current game's history is relevant, eliminating the need for permanent storage of past games.

## 4. User Identification

### Why it was chosen:
Using a combination of IP address and User Agent for user identification strikes a balance between simplicity and functionality. This method avoids the complexity of session cookies or other identity mechanisms, yet offers a reasonable level of uniqueness for user sessions in this gaming context.

## 5. Cross-Origin Resource Sharing (CORS)

### Why it was chosen:
CORS settings in the current configuration might seem permissive. However, it ensures that the game UI hosted on a different domain can interact seamlessly with the API. Future iterations would involve refining this setup for enhanced security, based on the deployment environment and known client domains.

## 6. Future Considerations & Caveats

- **Data Persistence**: As the game evolves or if there's a need to analyze game history, integrating a more permanent datastore would become essential.
- **Refining CORS**: Current CORS settings are broad for ease of integration and testing. They would need adjustments in a production environment.
