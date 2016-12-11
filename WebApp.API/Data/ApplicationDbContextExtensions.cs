using System.Linq;
using WebApp.API.Models.Db;

namespace WebApp.API.Data
{
    public static class ApplicationDbContextExtensions
    {
        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            if (!context.Businesses.Any())
            {

                context.Businesses.AddRange(
                    new Business() { Name = "Business 1" },
                    new Business() { Name = "Business 2" }
                );

                context.SaveChanges();
            }
        }
    }
}
