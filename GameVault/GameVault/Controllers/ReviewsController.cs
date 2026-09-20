using GameVault.Data;
using GameVault.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameVault.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly GameVaultDbContext dbContext;

        public ReviewsController(GameVaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Review> reviews = dbContext
                .Reviews
                .OrderByDescending(r => r.CreatedOn)
                .ThenByDescending(r => r.Rating)
                .ToArray();

            return View(reviews);
        }
    }
}
