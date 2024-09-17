using Application;
using Infrastructure;
using Infrastructure.Hubs;
using Infrastructure.Seeder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureService();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Now configure Autofac as the DI container
builder.Services.AddApplicationService();
builder.Services.AddHealthChecks();
builder.Services.AddTransient<DbSeeder>();
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapHealthChecks("/health");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seeder
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    seeder.Seed();
}

app.MapFallbackToFile("/index.html");
app.MapHub<ChatHub>("/chat");

app.Run();