using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using TPICAP.Persons.Domain.Persons;

namespace TPICAP.Persons.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Person { get; set; }

        private readonly IConfiguration _configuration;

        public AppDbContext(
            DbContextOptions<AppDbContext> dbContextOptions,
            IConfiguration configuration)
            : base(dbContextOptions)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("AppEntities"));
        }
    }
}
