using AdminProjectsDemo;
using AdminProjectsDemo.DataContext;
using AdminProjectsDemo.DataContext.Data;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment); 

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    DbInitializer.Initialize(services);
}

app.Run();
