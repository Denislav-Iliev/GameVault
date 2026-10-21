using GameVault.Data.Models;
using GameVault.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static GameVault.GCommon.PageValidation;

namespace GameVault.Controllers
{
    public class LibraryController : Controller
    {
        private readonly GameVaultDbContext dbContext;

        public LibraryController(GameVaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<UserGame> userGames = dbContext
                .UserGames
                .Include(g => g.Game)
                .OrderByDescending(ug => ug.HoursPlayed)
                .Take(EntitiesPerPage)
                .ToArray();

            return View(userGames);
        }
    }
}
