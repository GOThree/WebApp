using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApp.API.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
