using GameVault.Data;
using GameVault.Data.Models;
using GameVault.Services.Core.Models.Games;
using Microsoft.AspNetCore.Mvc;
using static GameVault.GCommon.PageValidation;

namespace GameVault.Controllers
{
    public class GenresController : Controller
    {
        private readonly GameVaultDbContext dbContext;

        public GenresController(GameVaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Genre> genres = dbContext
                .Genres
                .OrderBy(g => g.Id)
                .Take(EntitiesPerPage)
                .ToArray();

            return View(genres);
        }
    }
}
