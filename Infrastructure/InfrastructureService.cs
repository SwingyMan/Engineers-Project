using Azure;
using Azure.Communication.Email;
using Azure.Core;
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
using Serilog;

namespace Infrastructure;

public static class InfrastructureService
{
    public static void AddInfrastructureService(this IServiceCollection serviceCollection)
    {
        var keyvault = new SecretClient(new Uri("https://socialplatformkv.vault.azure.net/"),
            new DefaultAzureCredential());
        var dbkey = $"Host=socialplatformser.postgres.database.azure.com;Database=socialplatformdb;Username=marcin;Password={keyvault.GetSecret("dbkey").Value.Value}";
        var insightskey = keyvault.GetSecret("insightskey").Value.Value;
       var signalrkey = $"Endpoint=https://socialplatformsr.service.signalr.net;AccessKey={keyvault.GetSecret("signalrkey").Value.Value};Version=1.0;";
        serviceCollection.AddAzureClients(clientbuilder =>
            {
                clientbuilder.AddBlobServiceClient(new Uri("https://socialplatformsa.blob.core.windows.net/"));
                clientbuilder.AddSecretClient(new Uri("https://socialplatformkv.vault.azure.net/"));
                clientbuilder.AddContentSafetyClient(new Uri("https://westeurope.api.cognitive.microsoft.com/"));
                clientbuilder.AddEmailClient(new Uri("https://socialplatformcs.europe.communication.azure.com"));
                clientbuilder.AddTextAnalyticsClient(new Uri("https://westeurope.api.cognitive.microsoft.com/"));
                clientbuilder.AddTextTranslationClient(new DefaultAzureCredential());
                clientbuilder.UseCredential(new DefaultAzureCredential());
            }
        );
        serviceCollection.AddSerilog((services, lc) => lc
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console());
        serviceCollection.AddDbContext<SocialPlatformDbContext>(opt =>
            opt.UseNpgsql(dbkey));
        serviceCollection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        serviceCollection.AddScoped(typeof(IBlobInfrastructure), typeof(BlobInfrastructure));
        serviceCollection.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        serviceCollection.AddScoped(typeof(IPostsRepository), typeof(PostsRepository));
        serviceCollection.AddApplicationInsightsTelemetry(x=>x.ConnectionString=insightskey);
        serviceCollection.AddServiceProfiler();
        serviceCollection.AddSignalR()
            .AddAzureSignalR(signalrkey);
        serviceCollection.AddScoped<IEmailSender, EmailSender>();
        serviceCollection.AddTransient<TextContentSafetyService>();
    }
}