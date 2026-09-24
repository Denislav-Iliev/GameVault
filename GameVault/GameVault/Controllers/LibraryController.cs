using GameVault.Data;
using GameVault.Models;
using Microsoft.AspNetCore.Mvc;
using static GameVault.Common.PageValidation;

namespace GameVault.Controllers
{
    public class LibraryController : Controller
    {
        private readonly GameVaultDbContext dbContext;

        public LibraryController(GameVaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<UserGame> userGames = dbContext
                .UserGames
                .OrderByDescending(ug => ug.HoursPlayed)
                .Take(EntitiesPerPage)
                .ToArray();

            return View(userGames);
        }
    }
}
