using GameVault.Data;
using GameVault.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameVault.Controllers
{
    public class UserGamesController : Controller
    {
        private readonly GameVaultDbContext dbContext;

        public UserGamesController(GameVaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<UserGame> userGames = dbContext
                .UserGames
                .OrderByDescending(ug => ug.HoursPlayed)
                .ToArray();

            return View(userGames);
        }
    }
}
