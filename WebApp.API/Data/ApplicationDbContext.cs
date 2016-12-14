using Microsoft.EntityFrameworkCore;
using WebApp.API.Models.Db;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApp.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Business> Businesses { get; set; }
    }
}
