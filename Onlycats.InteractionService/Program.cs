using MongoDB.Driver;
using OnlycatsTFG.InteractionService.MongoRepository;
using OnlycatsTFG.PostService.Repositories;
using OnlycatsTFG.models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("mongoconnection");
var client = new MongoClient(connectionString);

var database = client.GetDatabase("activities");

builder.Services.AddScoped<IMongoRepository<Activity, string>>(
    provider => new ActivityMongoRepository<Activity, string>(database.GetCollection<Activity>("activities"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
