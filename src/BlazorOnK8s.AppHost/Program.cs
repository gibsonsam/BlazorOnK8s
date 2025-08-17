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
    .AddProject<Projects.BlazorOnK8s>("blazoronk8s", configure: static project =>
    {
        project.ExcludeLaunchProfile = true;
    })
    .WithHttpEndpoint(port: defaultPort, targetPort: defaultPort)
    .WithReplicas(3)
    .PublishAsKubernetesService((service) =>
    {
        service.WithIngress(port: defaultPort);
    });
// .WithReference(db)
// .WaitFor(db);

builder.Build().Run();
