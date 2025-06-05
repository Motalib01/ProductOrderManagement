# ProductOrderManagement

## Table of Contents

- [Project Overview](#project-overview)  
- [Technologies Used](#technologies-used)  
- [Project Structure](#project-structure)  
- [Setup Instructions](#setup-instructions)  
- [API Endpoints](#api-endpoints)  
- [Assumptions & Decisions](#assumptions--decisions)  
- [Future Enhancements](#future-enhancements)  
- [Contact](#contact)  

---

## Project Overview

This project implements a product order management system designed with the following key concepts:

- **CQRS Pattern**: Separation of commands (writes) and queries (reads) using MediatR.
- **Domain-Driven Design (DDD)**: Clear separation between Domain, Application, and Presentation layers.
- **Result Pattern**: Functional approach to error handling using `Result<T>` wrapping success/failure.
- **Pagination** support on query results.
- **Minimal API** endpoints for lightweight, fast REST API.

---

## Technologies Used

- **.NET 9** — Modern, cross-platform framework for building APIs.
- **MediatR** — In-process messaging to implement CQRS pattern.
- **ASP.NET Core Minimal APIs** — Lightweight API endpoints without full MVC overhead.
- **Dependency Injection** — For loose coupling and testability.
- **Domain-Driven Design (DDD)** — Organizes business logic cleanly.
- **Async/Await & CancellationTokens** — For scalable, responsive async programming.

---

## Project Structure

| Layer        | Responsibility                                               | Example Types                         |
|--------------|--------------------------------------------------------------|-------------------------------------|
| Domain       | Core business logic, entities, value objects, domain services | `Order`, `Product`, `PricingService` |
| Application  | Commands, Queries, Handlers, DTOs, application services       | `PlaceOrderCommand`, `GetOrdersQuery`, `OrderDto` |
| Presentation | REST API endpoints                                            | Minimal API endpoints under `/api/orders` |

---

## Setup Instructions

1. **Clone the repository**

```bash
git clone https://github.com/Motalib01/ProductOrderManagement.git
cd ProductOrderManagement
