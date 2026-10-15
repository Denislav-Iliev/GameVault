using GameVault.Data;
using GameVault.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {
            IEnumerable<Genre> genres = await dbContext
                .Genres
                .AsNoTracking()
                .OrderBy(g => g.Id)
                .Take(EntitiesPerPage)
                .ToArrayAsync();

            return View(genres);
        }
    }
}
