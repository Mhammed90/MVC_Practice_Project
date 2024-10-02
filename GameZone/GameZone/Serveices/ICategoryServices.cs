
namespace GameZone.Serveices
{
    public interface ICategoryServices
    {
        IEnumerable<SelectListItem> GetCategories();
    }
}