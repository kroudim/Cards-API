using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class CardForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        [MaxLength(200)]
        [RegularExpression(@"^#+[a-zA-Z0-9]+$")]
        [StringLength(7, MinimumLength = 7)]
        public string? Color { get; set; } = String.Empty;

        [RegularExpression("ToDo|InProgress|Done", ErrorMessage = "Invalid Status")]
        public string? Status { get; set; }
    }
}
