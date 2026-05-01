using Service.Services;
using Microsoft.EntityFrameworkCore;
using MongoDataLayerService;
using MongoDB.Driver;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.OpenTelemetry(options =>
    {
        options.Endpoint = "http://127.0.0.1:5304";
        options.ResourceAttributes = new Dictionary<string, object>
        {
            ["service.name"] = "test-logging-service",
            ["index"] = 10,
            ["flag"] = true,
            ["value"] = 3.14
        };
    })
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

var mongoUrl = new MongoUrl(builder.Configuration.GetConnectionString("MongoDbConnection"));
builder.Services.AddDbContext<MongoDbContext>(options => options
    .EnableSensitiveDataLogging()
    .UseMongoDB(mongoUrl.Url, mongoUrl.DatabaseName)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<MongoService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
