using Azure.Identity;
using Domain.Services;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureKeyVault(
    new Uri(builder.Configuration["KeyVaultConfiguration:KeyVaultURL"]),
    new DefaultAzureCredential()
    );


builder.Services.AddSignalR().AddAzureSignalR();
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
