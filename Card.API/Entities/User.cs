using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class User    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string? Password { get; set; }


        [MaxLength(200)]
        public string? Role { get; set; }


    public ICollection<Card> Cards { get; set; }
               = new List<Card>();

        public User(string email, string password, string role)
        {
         Email = email;
         Password =password;
         Role = role;
        }
    }
}
