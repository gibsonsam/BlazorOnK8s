using Aspire.Hosting.Kubernetes.Resources;

namespace Aspire.Hosting.Kubernetes;

public static class KubernetesExtensions
{
    public static void WithIngress(this KubernetesResource service, int port)
    {
        var ingress = new Ingress
        {
            Metadata = new ObjectMetaV1
            {
                Name = $"{service.Name}-ingress",
                Annotations = // https://learn.microsoft.com/en-us/aspnet/core/blazor/host-and-deploy/server#kubernetes
                {
                    { "nginx.ingress.kubernetes.io/affinity", "cookie" },
                    { "nginx.ingress.kubernetes.io/session-cookie-name", "affinity" },
                    { "nginx.ingress.kubernetes.io/session-cookie-expires", "14400" },
                    { "nginx.ingress.kubernetes.io/session-cookie-max-age", "14400" },
                }
            },
            Spec = new IngressSpecV1
            {
                IngressClassName = "webapprouting.kubernetes.azure.com", // https://learn.microsoft.com/en-us/azure/aks/app-routing
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