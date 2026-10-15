using GameVault.Data;
using GameVault.Data.Models;
using GameVault.Services.Core.Models.Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static GameVault.GCommon.PageValidation;

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
        public async Task<IActionResult> Index(
            [FromQuery] int? genreId,
            [FromQuery] string? searchTerm)
        {
            IQueryable<Game> gamesQuery = dbContext
                .Games
                .Include(g => g.Genre)
                .AsNoTracking();

            if (genreId != null)
            {
                gamesQuery = gamesQuery
                    .Where(g => g.GenreId == genreId);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                gamesQuery = gamesQuery
                    .Where(g => EF.Functions.Like(g.Title, $"%{searchTerm}%"));
            }

            IEnumerable<Game> games = await gamesQuery
                .OrderByDescending(g => g.ReleaseYear)
                .Take(EntitiesPerPage)
                .ToArrayAsync();

            ViewBag.GenreId = genreId;

            ViewBag.Genres = await LoadAllGenresAsync();

            return View(games);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Game? game = await dbContext
                .Games
                .Include(g => g.Genre)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            GameFormViewModel model = new GameFormViewModel
            {
                Genres = await LoadAllGenresAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Invalid data was detected!");

                model.Genres = await LoadAllGenresAsync();

                return View(model);
            }

            if (model.ReleaseYear > DateTime.Today.Year)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Invalid release year was detected!");

                model.Genres = await LoadAllGenresAsync();

                return View(model);
            }

            bool genreExists = await dbContext
                .Genres
                .AnyAsync(g => g.Id == model.GenreId);

            if (!genreExists)
            {
                ModelState.AddModelError(
                    nameof(model.GenreId),
                    "Invalid genre was selected!");

                model.Genres = await LoadAllGenresAsync();

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

            await dbContext.Games.AddAsync(newGame);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int? id)
        {
            if (!id.HasValue || id.Value <= 0)
            {
                return BadRequest(
                    "There was an error with your request! Please try again!");
            }

            Game? gameToEdit = await dbContext
                .Games
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id.Value);

            if (gameToEdit == null)
            {
                return NotFound();
            }

            GameFormViewModel model = new GameFormViewModel
            {
                Description = gameToEdit.Description,
                Title = gameToEdit.Title,
                ReleaseYear = gameToEdit.ReleaseYear,
                Developer = gameToEdit.Developer,
                ImageUrl = gameToEdit.ImageUrl,
                GenreId = gameToEdit.GenreId,
                Genres = await LoadAllGenresAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            [FromRoute] int id,
            GameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Invalid data was detected!");

                model.Genres = await LoadAllGenresAsync();

                return View(model);
            }

            if (model.ReleaseYear > DateTime.Today.Year)
            {
                ModelState.AddModelError(
                    nameof(model.ReleaseYear),
                    "Invalid release year was detected!");

                model.Genres = await LoadAllGenresAsync();

                return View(model);
            }

            bool genreExists = await dbContext
                .Genres
                .AnyAsync(g => g.Id == model.GenreId);

            if (!genreExists)
            {
                ModelState.AddModelError(
                    nameof(model.GenreId),
                    "Invalid genre was selected!");

                model.Genres = await LoadAllGenresAsync();

                return View(model);
            }

            Game? gameToEdit = await dbContext
                .Games
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gameToEdit == null)
            {
                return NotFound();
            }

            gameToEdit.Description = model.Description;
            gameToEdit.Developer = model.Developer;
            gameToEdit.GenreId = model.GenreId;
            gameToEdit.ImageUrl = model.ImageUrl;
            gameToEdit.ReleaseYear = model.ReleaseYear;
            gameToEdit.Title = model.Title;

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            if (!id.HasValue || id.Value <= 0)
            {
                return BadRequest(
                    "There was an error with your request! Please try again!");
            }

            Game? game = await dbContext
                .Games
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id.Value);

            if (game == null)
            {
                return NotFound();
            }

            GameDeleteViewModel model = new GameDeleteViewModel
            {
                Id = game.Id,
                Title = game.Title
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest(
                    "There was an error with your request! Please try again!");
            }

            Game? gameToDelete = await dbContext
                .Games
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gameToDelete == null)
            {
                return NotFound();
            }

            dbContext.Games.Remove(gameToDelete);

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<Genre>> LoadAllGenresAsync()
        {
            IEnumerable<Genre> allGenres = await dbContext
                .Genres
                .AsNoTracking()
                .OrderBy(g => g.Name)
                .ToArrayAsync();

            return allGenres;
        }
    }
}
