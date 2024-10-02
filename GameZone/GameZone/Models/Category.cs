namespace GameZone.Models
{
    public class Category :BaseEntity
    {

        public List<Game> Games { get; set; } = new List<Game>();
    }
}
