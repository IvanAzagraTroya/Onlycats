using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using Onlycats.PostService.Services;
using OnlycatsTFG;
using OnlycatsTFG.models;
using OnlycatsTFG.PostService.Controllers;
using OnlycatsTFG.PostService.MongoRepository;
using OnlycatsTFG.PostService.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var connectionString = builder.Configuration.GetConnectionString("mongoconnection");
var client = new MongoClient(connectionString);


var database = client.GetDatabase("onlycats");
var collectionNames = database.ListCollectionNames().ToList();

var requiredCollections = new List<string> { "posts", "activities", "comments" };

foreach (var collectionName in requiredCollections)
{
    if (!collectionNames.Contains(collectionName))
    {
        database.CreateCollection(collectionName);
    }
}

var postdb = database.GetCollection<Post>("posts");
builder.Services.AddScoped<IMongoRepository<Post, string>>(
    provider => new PostMongoRepository<Post, string>(postdb)
    );

var commentdb = database.GetCollection<Comment>("comments");
builder.Services.AddScoped<IMongoRepository<Comment, ObjectId>>(
    provider => new CommentMongoRepository<Comment, ObjectId>(commentdb)
    );
builder.Services.AddScoped<ImageService>();

builder.Services.AddDataProtection().PersistKeysToFileSystem(
    new DirectoryInfo(@"program/persist"))
    .SetApplicationName("Onlycats.Posts");

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider("/app/images")
//});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
