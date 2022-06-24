using application.Interfaces.Authentication;
using application.Services.Authentication;
using infrastructure.Configuration;
using infrastructure.Repository.Interfaces.User;
using infrastructure.Repository.Repositories.User;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// var _connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// builder.Services.AddEntityFrameworkMySql().AddDbContext<ContextBase>(
//     options => options.UseMySql(
//         _connectionString,
//         ServerVersion.AutoDetect(_connectionString)
//     ));

//Authentication
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthentication, AuthenticationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
