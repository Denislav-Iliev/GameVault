using GameVault.Data.Models;
using GameVault.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static GameVault.GCommon.PageValidation;

namespace GameVault.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly GameVaultDbContext dbContext;

        public ReviewsController(GameVaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Review> reviews = dbContext
                .Reviews
                .Include(g => g.Game)
                .OrderByDescending(r => r.CreatedOn)
                .ThenByDescending(r => r.Rating)
                .Take(EntitiesPerPage)
                .ToArray();

            return View(reviews);
        }
    }
}
