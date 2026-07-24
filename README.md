# AdventureWorks Microservices

A reference implementation of a microservices architecture using .NET 8, Docker, and Clean Architecture principles. This project demonstrates how to build scalable, distributed applications with modern technologies.

## Architecture Overview

The solution follows a **Microservices Architecture** style, where each service is self-contained and responsible for a specific business capability. The services communicate asynchronously via RabbitMQ and are exposed through an API Gateway.

### Core Architecture Patterns
- **Clean Architecture**: Separation of concerns into Domain, Application, Infrastructure, and API layers.
- **CQRS (Command Query Responsibility Segregation)**: Used within services to separate read and write operations (facilitated by MediatR).
- **API Gateway**: Centralized entry point using Ocelot.
- **Service Discovery**: Dynamic service registration and discovery using Consul.
- **Event-Driven Architecture**: Asynchronous communication between services using RabbitMQ.

## Services

| Service | Description | Technology/Database |
|BC|---|---|
| **Gateway API** | The entry point for all client requests. Handles routing and aggregation. | Ocelot, Consul |
| **Identity API** | Manages user authentication, registration, and JWT token generation. | MS SQL Server |
| **Sales API** | Handles sales-related operations and customer management. | MS SQL Server, Redis (Cache) |
| **Jobs API** | Manages background jobs and processing. | MS SQL Server |
| **Logging** | Centralized logging and event sourcing. | MongoDB (Request Logs, EventStore) |

## Technology Stack

- **Framework**: .NET 8
- **Containerization**: Docker & Docker Compose
- **Database**: 
  - Microsoft SQL Server 2022 (Relational data)
  - MongoDB (Logging & Event Sourcing)
  - Redis (Caching)
- **Messaging**: RabbitMQ (using `RabbitMQ.Client`)
- **Service Discovery**: Consul
- **Observability**: Seq (Centralized structured logging)
- **API Gateway**: Ocelot

## Prerequisites

Ensure you have the following installed on your machine:

- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

## Getting Started

1.  **Clone the repository**
    ```bash
    git clone <repository-url>
    cd Microservices
    ```

2.  **Run with Docker Compose**
    The easiest way to run the entire solution is using Docker Compose. It will spin up all services, databases, and infrastructure components.
    ```bash
    docker-compose up -d
    ```
    *Note: The first run might take a few minutes to download images and build containers.*

3.  **Verify Services**
    Check the status of your containers:
    ```bash
    docker-compose ps
    ```

## Usage

### Service Endpoints

The services are exposed via the API Gateway and directly (for development).

| Component | Local URL (HTTP) | Local URL (HTTPS) |
|---|---|---|
| **API Gateway** | `http://localhost:6001` | `https://localhost:6002` |
| **Identity API** | `http://localhost:6003` | `https://localhost:6004` |
| **Sales API** | `http://localhost:6005` | `https://localhost:6006` |
| **Jobs API** | `http://localhost:6019` | `https://localhost:6020` |

### Infrastructure Interfaces

| Component | URL | Credentials (if applicable) |
|---|---|---|
| **Consul UI** | `http://localhost:8500` | - |
| **RabbitMQ Management** | `http://localhost:15672` | `guest` / `guest` |
| **Seq (Logging)** | `http://localhost:5341` | - |

## Project Structure

```
src/
├── api/                # API Layer (Controllers, Program.cs)
│   ├── gateway/        # API Gateway
│   ├── identity/       # Identity Service API
│   ├── jobs/           # Jobs Service API
│   └── sales/          # Sales Service API
├── services/           # Application & Infrastructure Layers
│   ├── identity/       # Identity Domain & Logic
│   └── ...
└── common/             # Shared Libraries (Messaging, Contracts, etc.)
```
