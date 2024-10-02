


namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameServices _gameServices;
        private readonly ICategoryServices _categoryServices;

        public HomeController(IGameServices gameServices, ICategoryServices categoryServices)
        {
            _gameServices = gameServices;
           _categoryServices = categoryServices;
        }



        public IActionResult Index()
        {
            var games = _gameServices.GetAll();
            return View(games);
        }

      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
