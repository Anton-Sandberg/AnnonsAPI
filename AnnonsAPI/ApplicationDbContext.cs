using AnnonsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnonsAPI
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Ad> Ads { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }
    }
}
