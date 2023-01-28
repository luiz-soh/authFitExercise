using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using application.Interfaces.Authentication;
using application.Services.Authentication;
using Application.Interfaces.User;
using infrastructure.Repository.Interfaces.Token;
using infrastructure.Repository.Interfaces.User;
using infrastructure.Repository.Repositories.Token;
using infrastructure.Repository.Repositories.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Models.Configuration.TokenConfiguration;
using Models.Configuration.ConnectionString;
using System.Text;
using Infrastructure.Repository.Interfaces.Gym;
using Infrastructure.Repository.Repositories.Gym;
using Application.Interfaces.Gym;
using Application.Services.Gym;
using Infrastructure.Repository.Repositories.Plan;
using Infrastructure.Repository.Interfaces.Plan;
using Application.Services.Plan;
using Application.Interfaces.Plan;

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
builder.Services.AddScoped<IUserService, UserService>();

//Authentication
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

//Gym
builder.Services.AddScoped<IGymRepository, GymRepository>();
builder.Services.AddScoped<IGymService, GymService>();

//Plan
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IPlanService, PlanService>();

//Add DynamoDB configuration
var awsOptions = builder.Configuration.GetAWSOptions();
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddOptions();

//TODO ajustar origins quando publicar o site
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("corsapp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
