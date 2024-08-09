using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage;
using Domain.Interfaces;
using Infrastructure.Blobs;
using Infrastructure.ContentSafety;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureService
{
    public static void AddInfrastructureService(this IServiceCollection serviceCollection)
    {
        var keyvault = new SecretClient(new Uri("https://socialplatformkv.vault.azure.net/"),
            new DefaultAzureCredential());
        serviceCollection.AddAzureClients(clientbuilder =>
            {
                clientbuilder.AddBlobServiceClient(new Uri("https://socialplatformsa.blob.core.windows.net/"),
                    new StorageSharedKeyCredential("socialplatformsa", keyvault.GetSecret("storagekey").Value.Value));
                clientbuilder.AddSecretClient(new Uri("https://socialplatformkv.vault.azure.net/"));
                clientbuilder.AddContentSafetyClient(new Uri("https://westeurope.api.cognitive.microsoft.com/"),
                    new AzureKeyCredential(keyvault.GetSecret("moderatorkey").Value.Value));
            }
        );
        serviceCollection.AddDbContext<SocialPlatformDbContext>(opt =>
            opt.UseNpgsql(
                $"Host=socialplatformser.postgres.database.azure.com;Database=socialplatformdb;Username=marcin;Password={keyvault.GetSecret("dbkey").Value.Value}"));
        serviceCollection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        serviceCollection.AddScoped(typeof(IBlobInfrastructure), typeof(BlobInfrastructure));
        serviceCollection.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        serviceCollection.AddScoped(typeof(IPostsRepository), typeof(PostsRepository));
        serviceCollection.AddApplicationInsightsTelemetry(x=>x.ConnectionString=keyvault.GetSecret("insightskey").Value.Value);
        serviceCollection.AddServiceProfiler();
        serviceCollection.AddAzureClients(x =>
        {
            x.AddEmailClient(
                $"endpoint=https://socialplatformcs.europe.communication.azure.com/;accesskey={keyvault.GetSecret("emailkey").Value.Value}");
        });
        serviceCollection.AddSignalR()
            .AddAzureSignalR(
                $"Endpoint=https://socialplatformsr.service.signalr.net;AccessKey={keyvault.GetSecret("signalrkey").Value.Value};Version=1.0;");
        serviceCollection.AddScoped<IEmailSender, EmailSender>();
        serviceCollection.AddTransient<TextContentSafetyService>();
    }
}