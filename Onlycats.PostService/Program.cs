using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using OnlycatsTFG;
using OnlycatsTFG.models;
using OnlycatsTFG.PostService.Controllers;
using OnlycatsTFG.PostService.MongoRepository;
using OnlycatsTFG.PostService.Repositories;
using System;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// for testing the service without gateaway
var Config = builder.Configuration.AddJsonFile("Onlycats.PostService.appsettings.json").Build();
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

//string mongoConnectionString = File.ReadAllText("/run/secrets/mongodb_connection_string");
//builder.Configuration["ConnectionStrings:MongoDB"] = mongoConnectionString;
var connectionString = builder.Configuration.GetConnectionString("mongoconnection");
var client = new MongoClient(connectionString);

var database = client.GetDatabase("onlycats");
// testing the post db  
//var newPosts = new List<Post>
//    {
//        new Post
//        {
//            UserId = 1,
//            ImageUrl ="",
//        },
//       new Post
//        {
//            UserId = 2,
//            ImageUrl = "asd"
//        }
//    };

var postdb = database.GetCollection<Post>("posts");
//postdb.InsertMany(newPosts);
builder.Services.AddScoped<IMongoRepository<Post, ObjectId>>(
    provider => new PostMongoRepository<Post, ObjectId>(postdb)
    );

var commentdb = database.GetCollection<Comment>("comments");
builder.Services.AddScoped<IMongoRepository<Comment, ObjectId>>(
    provider => new CommentMongoRepository<Comment, ObjectId>(commentdb)
    );

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
