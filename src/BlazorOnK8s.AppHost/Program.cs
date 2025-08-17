var builder = DistributedApplication.CreateBuilder(args);

var k8s = builder.AddKubernetesEnvironment("k8s")
    .WithProperties(env =>
    {
        env.DefaultImagePullPolicy = "Always";
    });

// var db = builder
//     .AddSqlServer("mssql")
//     .AddDatabase("AppDb");

const int defaultPort = 8080;

builder
    .AddProject<Projects.BlazorOnK8s>("blazoronk8s").PublishAsKubernetesService((service) =>
    {
        service.WithIngress(port: defaultPort);
    });
// .WithReference(db)
// .WaitFor(db);

builder.Build().Run();
