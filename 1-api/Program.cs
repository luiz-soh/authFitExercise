using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using application.Interfaces.Authentication;
using application.Services.Authentication;
using infrastructure.Repository.Interfaces.Token;
using infrastructure.Repository.Interfaces.User;
using infrastructure.Repository.Repositories.Token;
using infrastructure.Repository.Repositories.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using models.Configuration.TokenConfiguration;
using Models.Configuration.ConnectionString;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TokenConfiguration>(
    builder.Configuration.GetSection(TokenConfiguration.Configuration));

string secret = builder.Configuration["TokenConfiguration:ClientSecret"]!;

var key = Encoding.ASCII.GetBytes(secret);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
        };
    });

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(ConnectionStrings.ConnectionString));
// Add services to the container.

//User
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Authentication
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IAuthentication, AuthenticationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add DynamoDB configuration
var awsOptions = builder.Configuration.GetAWSOptions();
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();

builder.Services.AddOptions();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
