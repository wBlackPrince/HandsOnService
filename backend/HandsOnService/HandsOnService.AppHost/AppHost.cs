using HandsOnService.AppHost;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder
    .AddProject<Projects.HandsOnService_Web>("api-service")
    .WithExternalHttpEndpoints()
    .WithSwaggerUI()
    .WithScalar()
    .WithReDoc();

builder.Build().Run();