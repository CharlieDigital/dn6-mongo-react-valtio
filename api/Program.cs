
var builder = WebApplication.CreateBuilder(args);

Console.WriteLine($"Sentry key: {builder.Configuration["Sentry__Dsn"]}");

// [CC] Add custom logging
builder.WebHost
    .UseSerilog((context, config) =>
    {
        config
            .WriteTo.Console()
            .WriteTo.Sentry(s => {
                s.MinimumBreadcrumbLevel = LogEventLevel.Debug;
                s.MinimumEventLevel = LogEventLevel.Debug;
            });
        //    .WriteTo.File("logs/log.txt");
        // Add other targets/sinks here.
        // Serilog has a variety of sinks: https://github.com/serilog/serilog/wiki/Provided-Sinks
        // See the docs here: https://github.com/serilog/serilog/wiki
    })
    .UseSentry(options => 
    {
        // Set using https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=linux
        // Example: "https://ed417*************a15e227e7616b2@o11****7.ingest.sentry.io/6****42"
        // dotnet user-secrets init
        // dotnet user-secrets set "Sentry__Dsn" "{KEY_VALUE}"
        options.Dsn = builder.Configuration["Sentry__Dsn"];
        options.Debug = true;
        options.SampleRate = 1;
        options.TracesSampleRate = 1;
        options.MaxRequestBodySize = RequestSize.Small;
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

app.UseSentryTracing();

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
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});

// [CC] Turn off for development
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
