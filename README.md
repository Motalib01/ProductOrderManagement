# Product & Order Management API

A simple API for managing products and customer orders using **.NET 8**, **Minimal APIs**, and **CQRS with MediatR**.

---

## Features

-  Manage **Products**  
-  Manage **Orders**  
-  CQRS Pattern using **MediatR**  
-  Clean Architecture structure  
-  Minimal APIs (no controllers)  
-  Swagger UI for easy testing  

---

## Setup Instructions

### 1️⃣ Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
- InMimory  
- IDE: Visual Studio 2022 

---

### 2️⃣ Clone the Repository

```bash
git clone https://github.com/Motalib01/ProductOrderManagement.git
cd ProductOrderManagement
```
## Open Swagger UI

Navigate to:  
`https://localhost:5001/swagger` or `http://localhost:5000/swagger`

You can test all endpoints from Swagger UI.

---

## Project Structure
/ProductOrderManagement.Application --> Application Layer (CQRS Handlers, Commands, Queries)  
/ProductOrderManagement.Domain --> Domain Entities (Product, Order, etc.)  
/ProductOrderManagement.Infrastructure --> Infrastructure (EF Core, DbContext, Repositories)  
/ProductOrderManagement.Presentation --> Presentation Layer (Minimal API Endpoints)  
/ProductOrderManagement.sln --> Solution file  

---

## Endpoints

### Products

- `POST /api/products` → Create product  
- `PUT /api/products/{id}/status` → Update product status  
- `GET /api/products` → Get paginated products  
- `GET /api/products/{id}` → Get product by ID  

### Orders

- `POST /api/orders` → Place order  
- `GET /api/orders` → Get paginated orders  
- `GET /api/orders/{id}` → Get order by ID  

---

## Assumptions & Decisions

- Used **CQRS** pattern with **MediatR** to separate commands and queries cleanly.  
- Used **Minimal APIs** instead of controllers for lightweight, modern API design.  
- Commands and queries are dispatched through MediatR.  
- EF Core used for data access with a DbContext initialized at startup.  
- No authentication or authorization included to keep it simple.  
- Database is auto-created and seeded on startup using a DbInitializer.

---

## Author

**Abd El-Motalib Chemouri**  
[abdelmotaliv.chemouri@gmail.com](mailto:abdelmotaliv.chemouri@gmail.com)

