using Microsoft.EntityFrameworkCore;
using UserAPI.Authorization;
using UserAPI.Helpers;
using UserAPI.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
 
    // use sql server db in production and sqlite db in development
    if (env.IsProduction())
        services.AddDbContext<DataContext>();
    else
        services.AddDbContext<DataContext, SqliteDataContext>();
 
    services.AddCors();
    services.AddControllers();

    // configure automapper with all automapper profiles from this assembly
    services.AddAutoMapper(typeof(Program));

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IUserService, UserService>();


    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

builder.WebHost.UseKestrel(opts=>{
    opts.Listen(IPAddress.Any,Convert.ToInt32(Environment.GetEnvironmentVariable("PORT")  ?? "5000"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();    
    dataContext.Database.Migrate();
}

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}

app.Run();
