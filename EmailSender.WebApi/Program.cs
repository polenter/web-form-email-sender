using EmailSender.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(options =>
{
    options.AddServerHeader = false;
});

// Add services to the container.
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);



var app = builder.Build();
startup.Configure(app, app.Environment);

app.Run();


