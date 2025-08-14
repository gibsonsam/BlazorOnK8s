var builder = DistributedApplication.CreateBuilder(args);

var k8s = builder.AddKubernetesEnvironment("k8s")
    .WithProperties(env =>
    {
        env.DefaultImagePullPolicy = "Always";
    });

// var db = builder
//     .AddSqlServer("mssql")
//     .AddDatabase("AppDb");

builder
    .AddProject<Projects.BlazorOnK8s>("blazoronk8s");
    // .WithReference(db)
    // .WaitFor(db);
    // .WithAnnotation<ContainerImageAnnotation>(new()
    // {
    //     Registry = "myaspireregistry.azurecr.io/myaspireregistry",
    //     Image = "blazoronk8s2",
    //     Tag = "latest"
    // }, ResourceAnnotationMutationBehavior.Replace);

builder.Build().Run();

