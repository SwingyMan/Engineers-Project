using Application.Commands;
using Application.DTOs;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Azure.Identity;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InfrastructureService();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

builder.Services.AddMediatR(typeof(GenericAddCommandHandler<Post, PostDTO>).Assembly);

// Now configure Autofac as the DI container
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // Here you can register other services directly with Autofac if needed
    // Example: containerBuilder.RegisterType<SomeService>().As<ISomeService>();
});

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");
app.MapHub<ChatHub>("/chat");

app.Run();
