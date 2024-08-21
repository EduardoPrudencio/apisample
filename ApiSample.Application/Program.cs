
using ApiSample.Application;
using ApiSample.Domain;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IMongoDBIntegrate>(sp =>
        new MongoDBIntegrate("mongodb://root:123456@localhost1:27017", "mudb"));

builder.Services.AddScoped<IMongoDatabase>(sp =>
    sp.GetRequiredService<IMongoDBIntegrate>().GetDatabaseConnection());

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IMongoDatabase>().GetCollection<Feed>("feeds"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
