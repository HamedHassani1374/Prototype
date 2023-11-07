using API.APIs;
using API;
using Carter;
using Persistance.JWTServices;
using Repository;
using Services.UserAuth;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
      .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
      .AddJsonFile("appsettings.json");

ConfigurationManager? Configuration = builder.Configuration;

var jwtSettings = Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
builder.Services.JWTRegisterService(jwtSettings);
builder.Services.Configure<JwtSettings>(Configuration.GetSection(nameof(JwtSettings)));
builder.Services.RegisterServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddCarter();

builder.Services.AddDbContext<ModelContext>(x => x.UseSqlServer(config.Build().GetConnectionString("ServiceConnection") ,
                                                                                    o => o.CommandTimeout(1000)));
#region Configure Serilog
var serlogconfig = new ConfigurationBuilder()
              .AddJsonFile("appsettings.Serilog.json")
              .Build();
Log.Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(serlogconfig)
           .CreateLogger();
builder.Host.UseSerilog();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseHttpsRedirection();
//app.MapCarter();
app.AddUserAuthEndPoint();
app.UseSerilogRequestLogging();

app.Run();


