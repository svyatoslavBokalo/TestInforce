using Backend.Data.ModelsConfiguration;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Backend.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        static public readonly string pathConfig = "appsettings.json";
        static public readonly string connectionString = "DefaultConnection";
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext() { }

        public DbSet<UrlInfo> UrlInfos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(pathConfig)
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString(connectionString));
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.LoginProvider, p.ProviderKey });
            builder.ApplyConfiguration(new UrlConfiguration());
        }
    }
}
