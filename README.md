# WeatherMicroservice

A .NET 8 microservice for retrieving and storing weather data using external APIs and a local database. 
Built with ASP.NET Core, Entity Framework Core, and follows clean architecture principles such as the Repository pattern for better separation of concerns.

## Features

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