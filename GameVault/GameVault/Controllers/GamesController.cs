using GameVault.Data;
using GameVault.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace GameVault.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameVaultDbContext dbContext;

        public GamesController(GameVaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Game> games = dbContext
                .Games
                .OrderByDescending(g => g.ReleaseYear)
                .ToArray();

            return View();
        }
    }
}
