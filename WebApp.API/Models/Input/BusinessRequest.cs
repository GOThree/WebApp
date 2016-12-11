using System.ComponentModel.DataAnnotations;

namespace WebApp.API.Models.Input
{
    public class BusinessRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
