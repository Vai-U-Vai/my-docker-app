using Countries_Cloud.Model;
using Microsoft.EntityFrameworkCore;


namespace Countries_Cloud.DB
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // Извлекаем имя подключения
                string connectionName = configuration["UseConnection"];
                if (string.IsNullOrEmpty(connectionName))
                {
                    throw new ArgumentException("Configuration 'UseConnection' is missing or empty.");
                }

                // Получаем строку подключения
                string connectionString = configuration.GetConnectionString(connectionName);
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ArgumentException($"Connection string '{connectionName}' is missing or empty.");
                }

                // Настраиваем Npgsql
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

    }
}
