
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.SignalR;

var builder = WebApplication.CreateBuilder(args);

// [CC] Add custom logging
builder.Host.UseSerilog((context, config) =>
{
    config
        .WriteTo.Console();
    //    .WriteTo.File("logs/log.txt");
    // Add other targets/sinks here.
    // Serilog has a variety of sinks: https://github.com/serilog/serilog/wiki/Provided-Sinks
    // See the docs here: https://github.com/serilog/serilog/wiki
});

// [CC] Add SignalR; see: https://docs.microsoft.com/en-us/aspnet/core/signalr/hubs?view=aspnetcore-6.0
// In production, consider using Azure SignalR services instead so that you don't have to manage
// this infrastructure.
// builder.Services.AddSignalR();

// [CC] Initialize Mongo: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-6.0&tabs=visual-studio-code
builder.Services.Configure<MongoDbConnectionSettings>(
    builder.Configuration.GetSection(nameof(MongoDbConnectionSettings))
);
builder.Services.AddSingleton<MongoDbContext>();

// [CC] Initialize the data services.
builder.Services.AddScoped<IDataServices, DataServices>();

// [CC] Add Authentication
builder.Services.Configure<AwsCognitoSettings>(
    builder.Configuration.GetSection(nameof(AwsCognitoSettings))
);

// [CC] See: https://stackoverflow.com/a/46963194
builder.Services
    .AddAuthentication(config => {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions: null);

builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, AwsCognitoJwtProvider>();

// Set up Azure SignalR
// https://docs.microsoft.com/en-us/azure/azure-signalr/signalr-quickstart-dotnet-core
builder.Services.Configure<AzureSettings>(
    builder.Configuration.GetSection(nameof(AzureSettings))
);

var azureSettings = builder.Configuration
    .GetSection(nameof(AzureSettings))
    .Get<AzureSettings>();

if (azureSettings.IsSignalRConfigured())
{
    builder.Services
        .AddSignalR()
        .AddAzureSignalR(options =>
        {
            options.Endpoints = new[]
            {
            new ServiceEndpoint(azureSettings.SignalRConnection)
            };
        });
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    // This pulls the code comments into the Swagger docs.
    // See: https://github.com/domaindrivendev/Swashbuckle.AspNetCore#include-descriptions-from-xml-comments
    string filePath = Path.Combine(System.AppContext.BaseDirectory, "Api.xml");
    config.IncludeXmlComments(filePath);
});

var app = builder.Build();

 app.UseRouting();

// [CC] In the container environment, we want to run this on 8080 for GCR
// This means that we will need to add the environment variable in the
// Dockerfile.
if(app.Environment.IsEnvironment("container"))
{
    app.Urls.Add("http://0.0.0.0:8080"); // Supoorts Google Cloud Run.
}

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseSwagger();
app.UseSwaggerUI();

// [CC] We need this to call our API from the static front-end
app.UseCors(options =>
{
    options.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials(); // This is required to pass credentials.
});

// [CC] Turn off for development
// app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Set up the SignalR hub.  This has to be AFTER the controllers are mapped.
app.UseEndpoints(e =>
{
    if (azureSettings.IsSignalRConfigured())
    {
        // See: https://docs.microsoft.com/en-us/aspnet/core/signalr/configuration?view=aspnetcore-6.0&tabs=dotnet#advanced-http-configuration-options
        e.MapHub<NotificationHub>("/notifications");
    }
});

app.Run();
