using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIREST.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        public int Id_user { get; set; }

        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string Role { get; set; }
    }
}
