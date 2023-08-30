using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class UserDBContext : DbContext
    {
        public UserDBContext() { }
        private static UserDBContext instance = null;
        private static readonly object _locker = new object();
        public static UserDBContext Instance
        {
            get
            {
                lock (_locker)
                {
                    if (instance == null)
                    {
                        instance = new UserDBContext();
                    }
                    return instance;
                }
            }
        }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin", Password = "admin123" },
                new User { Id = 2, Name = "User", Password = "user123" }
                );
        }
    }
}
