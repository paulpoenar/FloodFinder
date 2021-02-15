FloodFinder
https://floodfinder.azurewebsites.net
<br/>

## Overview
Built using an ASP.NET Core backend and an Angular frontend.

Prerequisites: .NET Core 5 (https://dotnet.microsoft.com/download/dotnet/5.0) and Node.js (https://nodejs.org/en/).

To install this example application, run the following commands:

https://github.com/paulpoenar/FloodFinder.git
This will download a copy of the project.
cd FloodFinder

Start the app
To install all of the dependencies and start the app, run:
dotnet run (this will do an npm install so might take a bit)

## Architecture

### Domain

Contains all entities and logic specific to the domain layer.

### Application

Defines interfaces that are implemented by outside layers and only dependent on the domain layer. 

### Infrastructure

Contains classes based on interfaces defined within the application layer.

### Main

Asp.NET Core specific stuff and the ClientApp (would have made this a separate project, but then would have ended with at least 3 repositories (Auth, Angular app, API), and thought it would be a bit of an overkill)

## Technologies

* ASP.NET Core 5
* [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [Angular 10](https://angular.io/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [XUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq) & [Respawn](https://github.com/jbogard/Respawn)
