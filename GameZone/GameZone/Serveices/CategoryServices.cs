
namespace GameZone.Serveices
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ApplicationDbContext _context;
        public CategoryServices(ApplicationDbContext context) => _context = context;

        public IEnumerable<SelectListItem> GetCategories()
        {
            return _context.Categories.Select(c => new SelectListItem
            { Value = c.Id.ToString(), Text = c.Name }).OrderBy(c => c.Text).AsNoTracking().ToList();
        }


    }
}



