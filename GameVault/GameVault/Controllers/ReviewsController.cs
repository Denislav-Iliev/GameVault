using GameVault.Data;
using GameVault.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static GameVault.Common.PageValidation;

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
                .Include(g => g.Game)
                .OrderByDescending(r => r.CreatedOn)
                .ThenByDescending(r => r.Rating)
                .Take(EntitiesPerPage)
                .ToArray();

            return View(reviews);
        }
    }
}
