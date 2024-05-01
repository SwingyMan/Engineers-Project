using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage;
using Infrastructure.Blobs;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Service
    {
        public static void InfrastructureService(this IServiceCollection serviceCollection)
        {
            var keyvault = new SecretClient(new Uri("https://socialplatformkv.vault.azure.net/"), new DefaultAzureCredential());
            serviceCollection.AddAzureClients(clientbuilder =>
            {
                clientbuilder.AddBlobServiceClient(new Uri("https://socialplatformsa.blob.core.windows.net/"), new StorageSharedKeyCredential("socialplatformsa", keyvault.GetSecret("storagekey").Value.Value));
            }
            );
            serviceCollection.AddDbContext<SocialPlatformDbContext>(opt =>opt.UseNpgsql($"Host=socialplatformser.postgres.database.azure.com;Database=socialplatformdb;Username=marcin;Password={keyvault.GetSecret("dbkey").Value.Value}"));
            serviceCollection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            serviceCollection.AddScoped(typeof(IBlobInfrastructure), typeof(BlobInfrastructure));

            serviceCollection.AddAzureClients(x=>
            {
                x.AddEmailClient($"endpoint=https://socialplatformcs.europe.communication.azure.com/;accesskey={keyvault.GetSecret("emailkey").Value.Value}");
            });
            serviceCollection.AddSignalR().AddAzureSignalR($"Endpoint=https://socialplatformsr.service.signalr.net;AccessKey={keyvault.GetSecret("signalrkey").Value.Value};Version=1.0;");
            serviceCollection.AddScoped<IEmailSender, EmailSender>();

        }
    }
}
