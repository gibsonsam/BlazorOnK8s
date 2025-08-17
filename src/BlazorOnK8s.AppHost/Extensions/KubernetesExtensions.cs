using Aspire.Hosting.Kubernetes;
using Aspire.Hosting.Kubernetes.Resources;

public static class KubernetesExtensions
{
    public static void WithIngress(this KubernetesResource service, int port)
    {
        // https://learn.microsoft.com/en-us/azure/aks/app-routing
        
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
                                            Port = new ServiceBackendPortV1 { Number = port }
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
    }
}