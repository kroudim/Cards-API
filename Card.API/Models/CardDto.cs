using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class CardDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Color { get; set; }
        public string? Status { get; set; }
        
        public DateTime CreationTime { get; set; } 
        public int UserId { get; set; }
    }
}
