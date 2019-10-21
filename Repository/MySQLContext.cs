using LinkCompressor.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkCompressor.Repository
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UrlView> UrlView { get; set; }
    }
}