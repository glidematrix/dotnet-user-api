namespace UserAPI.Helpers;

using Microsoft.EntityFrameworkCore;
using UserAPI.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server database
        // options.UseSqlServer(Configuration.GetConnectionString("UserAPIDatabase"));
        // options.UseNpgsql(Configuration.GetConnectionString("UserAPIDatabase"));
        options.UseNpgsql(GetHerokuConnectionString());
    }

    public DbSet<User> Users { get; set; }

    private static string GetHerokuConnectionString()
    {
        string connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

        var databaseUri = new Uri(connectionUrl);

        string db = databaseUri.LocalPath.TrimStart('/');
        string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

        return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
    }
}