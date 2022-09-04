using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TravelBook.Infrastructure;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.SetBasePath($"{Directory.GetParent(Directory.GetCurrentDirectory()).FullName}\\TravelBook.Web");
        builder.AddJsonFile("appsettings.json");
        IConfigurationRoot config = builder.Build();

        string connectionString = config.GetConnectionString("AppDbContext");
        optionsBuilder.UseNpgsql(connectionString);
        return new AppDbContext(optionsBuilder.Options);
    }
}

