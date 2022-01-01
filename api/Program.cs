
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
