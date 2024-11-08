using System.Text;
using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Secrets;
using Domain.Interfaces;
using Infrastructure.Blobs;
using Infrastructure.ContentSafety;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Azure.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Infrastructure;

public static class InfrastructureService
{
    public static void AddInfrastructureService(this IServiceCollection serviceCollection)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
        var keyvault = new SecretClient(new Uri("https://socialplatformkv.vault.azure.net/"),
            new DefaultAzureCredential());
        var dbkey = $"Host=socialplatformser.postgres.database.azure.com;Database=socialplatformdb;Username=marcin;Password={keyvault.GetSecret("dbkey").Value.Value}";
        var insightskey = keyvault.GetSecret("insightskey").Value.Value;
        serviceCollection.AddAzureClients(clientbuilder =>
            {
                clientbuilder.AddBlobServiceClient(new Uri("https://socialplatformsa.blob.core.windows.net/"));
                clientbuilder.AddSecretClient(new Uri("https://socialplatformkv.vault.azure.net/"));
                clientbuilder.AddContentSafetyClient(new Uri("https://safetext.cognitiveservices.azure.com/"));
                clientbuilder.AddEmailClient(new Uri("https://socialplatformcs.europe.communication.azure.com"));
                clientbuilder.AddTextAnalyticsClient(new Uri("https://westeurope.api.cognitive.microsoft.com/"));
                clientbuilder.AddTextTranslationClient(new DefaultAzureCredential());
                clientbuilder.UseCredential(new DefaultAzureCredential());
            }
        );
        
        serviceCollection.AddDbContext<SocialPlatformDbContext>(opt =>
            opt.UseNpgsql(dbkey));
        serviceCollection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Test.com",
                    ValidAudience = "Test.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwtkey"]))
                };
            });
        serviceCollection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        serviceCollection.AddScoped(typeof(IBlobInfrastructure), typeof(BlobInfrastructure));
        serviceCollection.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        serviceCollection.AddScoped(typeof(IPostsRepository), typeof(PostsRepository));
        serviceCollection.AddApplicationInsightsTelemetry(x=>x.ConnectionString=insightskey);
        serviceCollection.AddServiceProfiler();
        serviceCollection.AddSignalR()
            .AddAzureSignalR(x =>
            {
                x.Endpoints =
                [
                    new ServiceEndpoint(new Uri("https://socialplatformsr.service.signalr.net"),
                        new DefaultAzureCredential())
                ];
            });
        serviceCollection.AddScoped<IEmailSender, EmailSender>();
        serviceCollection.AddTransient<TextContentSafetyService>();
                serviceCollection.AddSerilog((services, lc) => lc
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console());
    }
}