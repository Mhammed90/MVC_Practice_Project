namespace GameZone.Models
{
    public class Game:BaseEntity
    { 
       

       
        [MaxLength(3000)]
        [Required]
        public string? Description { get; set; }

        [MaxLength(500)]
        [Required(ErrorMessage ="Game Cover is required .")]
        public string? Cover { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; } 


        public List<GameDevice> Devices { get; set;} = new List<GameDevice>();
    }
}
