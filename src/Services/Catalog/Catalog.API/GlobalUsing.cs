global using BuildingBlocks.Beharviors;
global using BuildingBlocks.CQRS;
global using BuildingBlocks.Exceptions;
global using Carter; // Minimal API
global using Catalog.API.Models;
global using Mapster; // Maping
global using Marten; // Handler with PostGreSql
global using MediatR; // Implement CQRS (Command Query Responsibility Segregation),
                      // pipeline inlucde: authenticate, handler, log, exception
global using Catalog.API.Exceptions;
global using FluentValidation; // Validate
global using Catalog.API.Data;
global using Marten.Pagination;
