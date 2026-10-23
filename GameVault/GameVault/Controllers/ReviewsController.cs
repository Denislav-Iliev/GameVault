using GameVault.Data.Models;
using GameVault.Services.Core.Contracts;
using GameVault.Services.Core.Models.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameVault.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewsController(
            IReviewService reviewService,
            UserManager<ApplicationUser> userManager)
        {
            this.reviewService = reviewService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Add(ReviewFormDto model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Games",
                    new { id = model.GameId });
            }

            string? userId = userManager.GetUserId(User);

            if (userId == null)
            {
                return Challenge();
            }

            await reviewService.AddReviewAsync(model, userId);

            return RedirectToAction("Details", "Games",
                new { id = model.GameId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string? userId = userManager.GetUserId(User);

            if (userId == null)
            {
                return Challenge();
            }

            ReviewFormDto? model =
                await reviewService.GetForEditAsync(id, userId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReviewFormDto model)
        {
            string? userId = userManager.GetUserId(User);

            if (userId == null)
            {
                return Challenge();
            }

            ReviewFormDto? existingReview =
                await reviewService.GetForEditAsync(model.Id, userId);

            if (existingReview == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                model.GameId = existingReview.GameId;
                return View(model);
            }

            bool updated = await reviewService.EditReviewAsync(model, userId);

            if (!updated)
            {
                return NotFound();
            }

            return RedirectToAction("Details", "Games",
                new { id = existingReview.GameId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string? userId = userManager.GetUserId(User);

            if (userId == null)
            {
                return Challenge();
            }

            ReviewFormDto? existingReview =
                await reviewService.GetForEditAsync(id, userId);

            if (existingReview == null)
            {
                return NotFound();
            }

            bool deleted = await reviewService.DeleteReviewAsync(id, userId);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction("Details", "Games",
                new { id = existingReview.GameId });
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = userManager.GetUserId(User);

            if (userId == null)
            {
                return Challenge();
            }

            IEnumerable<ReviewDto> reviews =
                await reviewService.GetUserReviewsAsync(userId);

            return View(reviews);
        }

    }
}
