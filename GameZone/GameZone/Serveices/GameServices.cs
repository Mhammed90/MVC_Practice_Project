using GameZone.Models;
using GameZone.Settings;

namespace GameZone.Serveices
{
    public class GameServices : IGameServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;
        public GameServices(ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = Path.Combine(_webHostEnvironment.WebRootPath, FileSettings.imagesPath);
        }
        public async Task CreateGame(CreateGameVeiwModle model)
        {
            var coverName = await CoverSave(model.Cover);

            Game game = new()
            {

                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList(),

            };
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();

        }




        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .Include(g => g.Category)
                .Include(d => d.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .ToList();
        }

        public Game? GetById(int id)
        {
            return _context.Games
               .Include(g => g.Category)
               .Include(d => d.Devices)
               .ThenInclude(d => d.Device)
               .AsNoTracking().SingleOrDefault(g => g.Id == id);
        }
        public async Task<Game?> Edit(EditGameViewModle modle)
        {
            var game = _context.Games
                .Include(g => g.Devices)
                .FirstOrDefault(g => g.Id == modle.Id);
            if (game is null)

                return null;

            var hasNewCover = modle.Cover is not null;
            var oldCover = game.Cover;

            game.Name = modle.Name;
            game.Description = modle.Description;
            game.CategoryId = modle.CategoryId;
            game.Devices = modle.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            if (hasNewCover)
            {
                game.Cover = await CoverSave(modle.Cover);
            }

            var effectedRows = await _context.SaveChangesAsync();

            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine("wwwroot", FileSettings.imagesPath, oldCover);
                    File.Delete(cover);
                }

                return game;

            }
            else
            {
                var cover = Path.Combine("wwwroot", FileSettings.imagesPath, game.Cover);
                File.Delete(cover);
                return null;
            }

        }
        public bool Delete(int id)
        {
            bool isDeleted = false;
            var game = _context.Games.FirstOrDefault(d => d.Id == id);
            if (game is null)
                return isDeleted;

            _context.Games.Remove(game);
            var eff = _context.SaveChanges();
            if(eff>0)
            {
                var cover = Path.Combine("wwwroot", FileSettings.imagesPath, game.Cover);
                File.Delete(cover);
                isDeleted = true;
              
            }
            return isDeleted;


        }

        private async Task<string> CoverSave(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagesPath, coverName);
            using (var stream = File.Create(path))
            {
                await cover.CopyToAsync(stream);
            }
            return coverName;
        }


    }

}