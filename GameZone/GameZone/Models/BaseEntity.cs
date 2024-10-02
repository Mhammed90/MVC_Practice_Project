using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage ="Name is required.")]
        public string? Name { get; set; }
    }
}
