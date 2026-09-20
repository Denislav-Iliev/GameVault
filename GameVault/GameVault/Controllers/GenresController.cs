using GameVault.Data;
using GameVault.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
                .ToArray();

            return View(genres);
        }
    }
}
