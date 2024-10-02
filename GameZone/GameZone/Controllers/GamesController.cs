
namespace GameZone.Controllers
{
    public class GamesController : Controller
    {


        private readonly ApplicationDbContext _context;
        private readonly ICategoryServices _categoryService;
        private readonly IDeviceServices _deviceServices;
        private readonly IGameServices _gameServices;

        public GamesController(ApplicationDbContext context,
           ICategoryServices categoryService,
           IDeviceServices deviceServices,
           IGameServices gameServices)
        {

            _context = context;
            _categoryService = categoryService;
            _deviceServices = deviceServices;
            _gameServices = gameServices;
        }


        public IActionResult Index()
        {
            var games = _gameServices.GetAll();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            Game game = _gameServices.GetById(id);
            if (game == null)
                return NotFound();
            return View(game);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var modelVeiw = new CreateGameVeiwModle()
            {
                Categories = _categoryService.GetCategories(),
                Devices = _deviceServices.GetDevices(),

            };

            return View(modelVeiw);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameVeiwModle modle)
        {
            if (ModelState.IsValid)
            {
                modle.Categories = _categoryService.GetCategories();

                modle.Devices = _deviceServices.GetDevices();
                return View(modle);

            }

            await _gameServices.CreateGame(modle);

            return RedirectToAction(nameof(Index));
        }

      //  [HttpDelete]
        public IActionResult Delete(int id)
        {
            bool isDeleted = _gameServices.Delete(id);
            if (isDeleted)
                return Ok();

            return BadRequest();

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gameServices.GetById(id);
            if (game == null)
                return NotFound();
            EditGameViewModle viewModle = new()
            {
                Id = id,
                Name = game.Name!,
                Description = game.Description!,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoryService.GetCategories(),
                Devices = _deviceServices.GetDevices(),
                CurrentCover = game.Cover

            };

            return View(viewModle);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameViewModle modle)
        {
            if (ModelState.IsValid)
            {
                modle.Categories = _categoryService.GetCategories();

                modle.Devices = _deviceServices.GetDevices();
                return View(modle);

            }

            var game = await _gameServices.Edit(modle);
            if (game == null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }
    }
}
