using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class Card    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }


        [MaxLength(200)]
        [RegularExpression(@"^([a-zA-Z0-9])$")]
        [StringLength(7,MinimumLength = 7)]
        public string? Color { get; set; }

        [MaxLength(200)]
        public string? Status { get; set; }


        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int UserId { get; set; }

        public DateTime CreationTime { get; set; }

        public Card(string name, string description,string color, string status )
        {
            Name = name;
            Description = description;
            Color = color;
            Status = status;
            CreationTime = DateTime.Now;
        }
    }
}
