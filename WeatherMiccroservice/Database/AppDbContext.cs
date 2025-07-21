using Microsoft.EntityFrameworkCore;
using WeatherMicroservice.Models.Entities;

namespace WeatherMicroservice.Database
{
    public class AppDbContext : DbContext
    {
        private IConfiguration _configuration { get; }

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DatabaseConnection"));
        }

        public DbSet<WeatherData> WeatherData { get; set; }
    }
}
