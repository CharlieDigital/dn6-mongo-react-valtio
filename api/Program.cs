
var builder = WebApplication.CreateBuilder(args);

// [CC] Add custom logging
builder.Host.UseSerilog((context, config) => {
    config
        .WriteTo.Console()
        .WriteTo.File("logs/log.txt");
    // Add other targets/sinks here.
    // Serilog has a variety of sinks: https://github.com/serilog/serilog/wiki/Provided-Sinks
    // See the docs here: https://github.com/serilog/serilog/wiki
});

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// [CC] We need this to call our API from the static front-end
app.UseCors(options => {
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});

// [CC] Turn off for development
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
