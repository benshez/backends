using Shezzy.Firebase;
using Serilog;

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Debug()
	.WriteTo.File("logs/rumble-.txt", rollingInterval: RollingInterval.Day)
	.CreateLogger();

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, builder.Environment);
