using Microsoft.EntityFrameworkCore;
using Weather_API_Wrapper_Service.Entities;

namespace Weather_API_Wrapper_Service.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<WeatherLog> WeatherLogs { get; set; }
    }
}
