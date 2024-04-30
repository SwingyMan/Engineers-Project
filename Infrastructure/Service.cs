using Azure.Storage;
using Infrastructure.Blobs;
using Infrastructure.IRepositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Service
    {
        public static void InfrastructureService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAzureClients(clientbuilder =>
            {
                clientbuilder.AddBlobServiceClient(new Uri("https://socialplatformsa.blob.core.windows.net/"), new StorageSharedKeyCredential("projectmanagementapp", "JS2TyBNCQS7ahbguXddJLj9a9xeUXlp7ZiZ8TlykySONbURITIKVyM0+vM395X7fwuMH/ZkKBxJ2+AStML6mvQ=="));
            }
            );
            serviceCollection.AddDbContext<Context.Context>();
            serviceCollection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            serviceCollection.AddScoped(typeof(IBlobInfrastructure), typeof(BlobInfrastructure));
        }
    }
}
