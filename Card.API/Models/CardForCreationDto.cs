using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class CardForCreationDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

        [MaxLength(200)]
        [RegularExpression(@"^#+[a-zA-Z0-9]+$")]
        [StringLength(7, MinimumLength = 7)]
        public string? Color { get; set; } = String.Empty;

        public int UserId { get; set; } 

        public string? Status { get; set; } = "ToDo";

        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
