using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Onlycats.UserService.Utils;
using OnlycatsTFG.models;
using OnlycatsTFG.repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var Config = builder.Configuration.AddJsonFile("Onlycats.UserService.appsettings.json").Build();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//string postgresConnectionString = File.ReadAllText("/run/secrets/postgres_connection_string");
//builder.Configuration["ConnectionStrings:Postgres"] = postgresConnectionString;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped(typeof(IRepository<int, User>), typeof(UserRepository<int, User>));

var app = builder.Build();

//var context = app.Services.GetRequiredService<ApplicationDbContext>();
//context.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
