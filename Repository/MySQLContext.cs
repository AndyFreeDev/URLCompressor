using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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