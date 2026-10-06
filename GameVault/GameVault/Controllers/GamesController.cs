using GameVault.Data;
using GameVault.ViewModels;
using GameVault.ViewModels.Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using static GameVault.Common.PageValidation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace GameVault.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameVaultDbContext dbContext;

        public GamesController(GameVaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index([FromQuery]int? genreId, [FromQuery]string? searchTerm)
        {
            var gamesQuery = dbContext
                    .Games
                    .Include(g => g.Genre)
                    .AsQueryable();

            if (genreId != null)
            {
                gamesQuery = gamesQuery
                    .Where(g => g.GenreId == genreId);
            }
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                gamesQuery = gamesQuery.Where(e => EF.Functions.Like(e.Title, $"%{searchTerm}%"));
            }


                IEnumerable<Game> games = gamesQuery
                    .OrderByDescending(g => g.ReleaseYear)
                    .Take(EntitiesPerPage)
                    .ToArray();

                ViewBag.GenreId = genreId;


                ViewBag.Genres = dbContext
                    .Genres
                    .OrderBy(g => g.Name)
                    .ToArray();

            return View(games);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Game? game = dbContext
                .Games
                .Include(gr => gr.Genre)
                .FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);

        }

        [HttpGet]
        public IActionResult Create()
        {
            GameFormViewModel model = new();

            model.Genres = dbContext
                .Genres
                .OrderBy(g => g.Name)
                .ToArray();
            
            return View(model);
        }

        [HttpPost] 
        public IActionResult Create(GameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid data was detected!");
                model.Genres = LoadAllGenres();

                return View(model);
            }

            if (model.ReleaseYear > DateTime.Today.Year)
            {
                ModelState.AddModelError(string.Empty, "Invalid release year was detected!");
                model.Genres = LoadAllGenres();

                return View(model);
            }

            bool genreExists = dbContext
                .Genres
                .Any(g => g.Id == model.GenreId);

            if (!genreExists)
            {
                ModelState.AddModelError(nameof(model.GenreId), "Invalid genre was selected!");
                model.Genres = LoadAllGenres();

                return View(model);
            }

            Game newGame = new Game
            {
                Title = model.Title,
                Description = model.Description,
                ReleaseYear = model.ReleaseYear,
                Developer = model.Developer,
                ImageUrl = model.ImageUrl,
                GenreId = model.GenreId
            };

            dbContext.Games.Add(newGame);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int? id)
        {
            if (!id.HasValue || id.Value <= 0)
            {
                return BadRequest("There was an error with your request! Please try again!");
            }

            Game? gameToEdit = dbContext
                .Games
                .FirstOrDefault(g => g.Id == id.Value);

            if (gameToEdit == null)
            {
                return NotFound();
            }

            GameFormViewModel model = new GameFormViewModel()
            {
                Description = gameToEdit.Description,
                Title = gameToEdit.Title,
                ReleaseYear = gameToEdit.ReleaseYear,
                Developer = gameToEdit.Developer,
                ImageUrl = gameToEdit.ImageUrl,
                GenreId = gameToEdit.GenreId,

                Genres = LoadAllGenres()
            };

            return View(model);
        }

        private IEnumerable<Genre> LoadAllGenres()
        {
            IEnumerable<Genre> allGenres = dbContext
                .Genres
                .OrderBy(g => g.Name)
                .ToArray();

            return allGenres;
        }
    }
}
