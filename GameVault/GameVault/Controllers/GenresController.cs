using GameVault.Data;
using GameVault.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static GameVault.Common.PageValidation;

namespace GameVault.Controllers
{
    public class GenresController : Controller
    {
        private readonly GameVaultDbContext dbContext;

        public GenresController(GameVaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Genre> genres = dbContext
                .Genres
                .OrderBy(g => g.Name)
                .Take(EntitiesPerPage)
                .ToArray();

            return View(genres);
        }
    }
}
