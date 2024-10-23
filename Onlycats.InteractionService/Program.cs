using MongoDB.Driver;
using OnlycatsTFG.InteractionService.MongoRepository;
using OnlycatsTFG.repository.mongorepository;
using OnlycatsTFG.models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var Config = builder.Configuration.AddJsonFile("appsettings.json").Build();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Config["Jwt:Issuer"],
        ValidAudience = Config["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]))
    };
});

var connectionString = builder.Configuration.GetConnectionString("mongoconnection");
var client = new MongoClient(connectionString);

var database = client.GetDatabase("onlycats");
//testing db
//var newActivities = new List<Activity>
//{
//    new Activity
//    {
//        PostId = 1,
//        UserId = 1,
//        ActionType = ActivityType.Like
//    },
//    new Activity
//    {
//        PostId = 2,
//        UserId = 2,
//        ActionType = ActivityType.Comment
//    }
//};
var activitiesDb = database.GetCollection<Activity>("activities");
//activitiesDb.InsertMany(newActivities);


builder.Services.AddScoped<IMongoRepository<Activity, ObjectId>>(
    provider => new ActivityMongoRepository<Activity, ObjectId>(activitiesDb)
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
