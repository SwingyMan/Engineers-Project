﻿using System.Text;
using Autofac.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Secrets;
using Domain.Interfaces;
using Infrastructure.Blobs;
using Infrastructure.ContentSafety;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.SignalR;
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
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken)
                            && path.StartsWithSegments("/chat"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
                options.Authority = "https://www.test.com";
            });
        serviceCollection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        serviceCollection.AddScoped(typeof(IBlobInfrastructure), typeof(BlobInfrastructure));
        serviceCollection.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        serviceCollection.AddScoped(typeof(IPostsRepository), typeof(PostsRepository));
        serviceCollection.AddScoped(typeof(IChatRepository), typeof(ChatRepository));
        serviceCollection.AddScoped(typeof(IChatMessageRepository), typeof(ChatMessageRepository));
        serviceCollection.AddScoped(typeof(IGroupRepository), typeof(GroupRepository));
        serviceCollection.AddScoped(typeof(IGroupPostRepository), typeof(GroupPostRepository));
        serviceCollection.AddScoped(typeof(IGroupUserRepository), typeof(GroupUserRepository));
        serviceCollection.AddScoped(typeof(IMessageRepository), typeof(MessageRepository));
        serviceCollection.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
        serviceCollection.AddApplicationInsightsTelemetry(x => x.ConnectionString = insightskey);
        serviceCollection.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
        serviceCollection.AddApplicationInsightsTelemetry(x=>x.ConnectionString=insightskey);
        serviceCollection.AddServiceProfiler();
        //serviceCollection.AddSignalR().AddAzureSignalR(options => { options.Endpoints = [new ServiceEndpoint(new Uri("https://socialplatformsr.service.signalr.net"), new DefaultAzureCredential())]; });
        serviceCollection.AddSignalR();

        serviceCollection.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins",
                builder =>
                {
                    builder.WithOrigins("http://127.0.0.1:5500", "http://127.0.0.1:5501")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
        });
        serviceCollection.AddScoped<IEmailSender, EmailSender>();
        serviceCollection.AddTransient<TextContentSafetyService>();
        serviceCollection.AddSerilog((services, lc) => lc
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());
    }
}