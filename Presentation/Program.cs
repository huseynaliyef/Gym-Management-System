using Data.Entities;
using Data.Extensions;
using Logic.Background_Services;
using Logic.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Presentation.Custom_MIddlewares;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using System.Configuration;
using System.Net;
using System.Reflection;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json").Build();

string constring = configuration.GetConnectionString("Default");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
    .WriteTo.MSSqlServer(
        connectionString: constring,
        tableName: "SerilogLogs",
        autoCreateSqlTable: true
    ).CreateLogger();


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

string connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Gym Management System", Version = "v1" });
    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Name = "Bearer",
            In = ParameterLocation.Header,
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            }
        },
        new List<string>()
    }
});
});
builder.Services.DbInject(builder.Configuration);
builder.Services.LogicServices();
builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.SuppressModelStateInvalidFilter = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionHandling>();
app.UseMiddleware<CheckAuthorizeMiddleWare>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
