using Aspire.Hosting.Kubernetes.Resources;

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
    .AddProject<Projects.BlazorOnK8s>("blazoronk8s").PublishAsKubernetesService((service) =>
    {
        var ingress = new Ingress
        {
            Metadata = new ObjectMetaV1
            {
                Name = $"{service.Name}-ingress"
            },
            Spec = new IngressSpecV1
            {
                IngressClassName = "webapprouting.kubernetes.azure.com",
                Rules =
                {
                    new IngressRuleV1
                    {
                        Host = null,
                        Http = new HttpIngressRuleValueV1
                        {
                            Paths =
                            {
                                new HttpIngressPathV1
                                {
                                    Path = "/",
                                    PathType = "Prefix",
                                    Backend = new IngressBackendV1
                                    {
                                        Service = new IngressServiceBackendV1
                                        {
                                            Name = $"{service.Name}-service",
                                            Port = new ServiceBackendPortV1 { Number = 8080 }
                                        },
                                        Resource = null
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        service.AdditionalResources.Add(ingress);
    });
// .WithReference(db)
// .WaitFor(db);

builder.Build().Run();

