using GameVault.Data;
using GameVault.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static GameVault.Common.PageValidation;
namespace GameVault.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameVaultDbContext dbContext;

        public GamesController(GameVaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index(int? genreId)
        {
            if (genreId == null)
            {
                IEnumerable<Game> games = dbContext
                    .Games
                    .Include(gr => gr.Genre)
                    .OrderByDescending(g => g.ReleaseYear)
                    .Take(EntitiesPerPage)
                    .ToArray();

                return View(games);
            }
            else
            {
                IEnumerable<Game> games = dbContext
                    .Games
                    .Where(g => g.GenreId == genreId)
                    .Include(gr => gr.Genre)
                    .OrderByDescending(g => g.ReleaseYear)
                    .Take(EntitiesPerPage)
                    .ToArray();

                ViewBag.GenreId = genreId;

                return View(games);
            }
        }

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
    }
}
