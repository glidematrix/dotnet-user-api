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
        options.UseNpgsql(Configuration.GetConnectionString("UserAPIDatabase"));
    }

    public DbSet<User> Users { get; set; }
}