var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.HandsOnService_Web>("handsonservice-web");

builder.Build().Run();