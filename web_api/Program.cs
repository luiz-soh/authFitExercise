using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using application.Interfaces.Authentication;
using application.Services.Authentication;
using infrastructure.Repository.Interfaces.Token;
using infrastructure.Repository.Interfaces.User;
using infrastructure.Repository.Repositories.Token;
using infrastructure.Repository.Repositories.User;
using models.Configuration.TokenConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TokenConfiguration>(
    builder.Configuration.GetSection(TokenConfiguration.Configuration));

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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
