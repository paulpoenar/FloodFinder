<br/>

## Overview
Built using an ASP.NET Core backend and an Angular frontend.

Prerequisites: .NET Core 5 (https://dotnet.microsoft.com/download/dotnet/5.0) and Node.js (https://nodejs.org/en/).

To install this example application, run the following commands:

https://github.com/paulpoenar/FloodFinder.git

This will download a copy of the project.

Start the app
1. Navigate to `FloodFinder/FloodFinder/ClientApp` and run `npm install`
2. Navigate to `FloodFinder/FloodFinder/ClientApp` and run `npm start` to launch the Angular app
3. Navigate to `FloodFinder/FloodFinder` and run `dotnet run` to launch Identity Server and the API

## Architecture

### Core

Contains all entities and logic specific to the domain layer.

### Application

Defines interfaces that are implemented by outside layers and only dependent on the domain layer. 

### Infrastructure

Contains classes based on interfaces defined within the application layer.

### Main

Presentation Layer, Main API, AuthServer and the ClientApp (3 separate projects in a real world scenario)

## Technologies

* [ASP.NET Core 5](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0)
* [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [Angular](https://angular.io/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [XUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq) & [Respawn](https://github.com/jbogard/Respawn)
