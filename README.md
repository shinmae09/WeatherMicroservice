# WeatherMicroservice

A .NET 8 microservice for retrieving and storing weather data using external APIs and a local database. 
Built with ASP.NET Core, Entity Framework Core, and follows clean architecture principles.

## Approach
This project is a simple Weather API that collects weather data from an external API (OpenWeatherMap) and stores it in a local database. 
It allows users to retrieve current weather data for a specified city or country, as well as historical weather data by date. 

The goal was to build a clean and maintainable structure that can be easily extended with a focus on clarity and better separation of concerns.

In this project, I created models to represent the weather data, and I added a service layer to handle the logic. 
I also followed the repository pattern to keep the data access logic separate from the business logic, and used dependency injection to manage services and dependencies, which makes it easier to test and maintain.

I just tred to keep the code simple, organized and straightforward in a way that makes it easy to add more features later, focusing on the core functionality without unnecessary complexity.

## Features
These are the main features of the WeatherMicroservice:
- Retrieve current weather data for a city/country via external API (OpenWeatherMap).
- Save current weather to a local database.
- Lookup historical weather data by date from the local database.
- RESTful API endpoints with Swagger/OpenAPI documentation.
- Data persistence using Entity Framework Core.
- Extensible service/repository architecture.

## Technologies
These are the main technologies used in this project:
- .NET 8
- C# 12
- ASP.NET Core Web API
- Entity Framework Core
- Swagger/OpenAPI
- Dependency Injection

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server or another supported database
- (Optional) Visual Studio 2022

### Setup

1. **Clone the repository:**