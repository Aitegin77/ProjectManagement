
# 📁 ProjectManagement

> ⚠️ **This project is currently under active development.**  
> Only the core project service has been implemented so far.  
> The architecture is well-structured and designed for scalability and maintainability.

## 🚀 Overview

**ProjectManagement** is a scalable and modular application for managing projects.  
It follows a clean, layered architecture with clearly separated responsibilities, aiming for high code quality and future extensibility.

## ✅ Current Features

- Full **CRUD operations** for the `Project` entity  
- Clean **layered architecture**  
- Implementation of the **Repository pattern**  
- Configured project structure with modular layers  
- Basic domain and service logic implemented  
- Unit testing with **xUnit**

## 🧱 Architecture

The project is built with a multi-layered architecture, including:

- `API` — Presentation layer (ASP.NET Core Web API)  
- `BLL` — Business Logic Layer  
- `DAL` — Data Access Layer (Entity Framework Core)  
- `DTO` — Data Transfer Objects
- `Common` — Shared utilities and constants  
- `Tests` — Unit tests (xUnit)

This structure allows for loose coupling and easy testing, maintenance, and future scaling.

## 🛠️ Tech Stack

- **Language**: C#  
- **Framework**: ASP.NET Core Web API  
- **ORM**: Entity Framework Core  
- **Unit Testing**: xUnit  
- **Mapping**: Mapster  
- **Database**: Microsoft SQL Server

## 🧪 Testing

- Project is covered with **unit tests** using `xUnit`  
- Testable architecture with mockable services and repositories

## 📦 Getting Started


- Configure your database connection string in `appsettings.json/appsettings.Development.json`.
- Run the project!

## 📌 Roadmap

- 🔐 Add authentication and authorization  
- 👥 Employee management  
- 🗂️ Task and milestone tracking  
- 🌐 Frontend UI (planned)

## 👨‍💻 Author

**Aitegin**  
[GitHub](https://github.com/Aitegin77)
---
_If you like this project, feel free to ⭐ the repository!_
