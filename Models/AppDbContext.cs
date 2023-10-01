using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<WalkthroughStep> WalkthroughSteps { get; set; }
    }

}
