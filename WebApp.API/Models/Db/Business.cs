using System.ComponentModel.DataAnnotations;

namespace WebApp.API.Models.Db
{
    public class Business
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

