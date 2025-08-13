var builder = DistributedApplication.CreateBuilder(args);

var db = builder
    .AddSqlServer("mssql")
    .AddDatabase("AppDb");

builder
    .AddProject<Projects.BlazorOnK8s>("blazoronk8s")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
