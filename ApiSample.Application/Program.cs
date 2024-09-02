
using ApiSample.Application;
using ApiSample.Domain;
using MongoDB.Driver;

string _mongoConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTIONSTRING") ?? "mongodb://root:123456@localhost:27017";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IMongoDBIntegrate>(sp =>
        new MongoDBIntegrate(_mongoConnectionString, "mudb"));

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
