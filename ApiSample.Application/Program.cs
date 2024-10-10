
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()     // Permite qualquer origem (frontend)
                   .AllowAnyMethod()     // Permite qualquer método HTTP (GET, POST, etc.)
                   .AllowAnyHeader();    // Permite qualquer cabeçalho
        });
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");

// app.MapGet("/api/news/pagenumber/{page}", (int page) =>
// {
//     var news = new List<object>
//     {
//         new {
//             id = "670037caee2ca803dad76901",
//             author = "Autor Desconhecido",
//             title = "Notícias da Nave #262...",
//             content = "<p>Conteúdo da notícia...</p>",
//             link = "https://example.com",
//             image = "https://example.com/image.jpg",
//             publishDate = "2024-10-04T18:45:07Z"
//         }
//         // Outras notícias podem ser adicionadas aqui
//     };

//     return Results.Ok(news);
// });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
