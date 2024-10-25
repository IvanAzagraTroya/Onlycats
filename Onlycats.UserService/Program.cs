using Microsoft.EntityFrameworkCore;
using Onlycats.UserService.Utils;
using OnlycatsTFG.models;
using OnlycatsTFG.repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//string postgresConnectionString = File.ReadAllText("/run/secrets/postgres_connection_string");
//builder.Configuration["ConnectionStrings:Postgres"] = postgresConnectionString;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IRepository<int, User>), typeof(UserRepository<int, User>));

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

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
