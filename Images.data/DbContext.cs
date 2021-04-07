using Microsoft.EntityFrameworkCore;
using System;

namespace Images.data
{
    public class ImagesDbContext : DbContext
    {
        private string _connectionString;
        public ImagesDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        public DbSet<Image> Images { get; set; }
    }
}
