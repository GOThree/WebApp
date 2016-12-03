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
                    new Business(),
                    new Business()
                );

                context.SaveChanges();
            }
        }
    }
}
