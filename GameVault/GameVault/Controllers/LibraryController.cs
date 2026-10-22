using GameVault.Data.Models;
using GameVault.Services.Core.Contracts;
using GameVault.Services.Core.Models.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameVault.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        private readonly ILibraryService libraryService;
        private readonly UserManager<ApplicationUser> userManager;

        public LibraryController(
            ILibraryService libraryService,
            UserManager<ApplicationUser> userManager)
        {
            this.libraryService = libraryService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = userManager.GetUserId(User);

            if (userId == null)
            {
                return Challenge();
            }

            var userGames = await libraryService.GetUserLibraryAsync(userId);

            return View(userGames);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(int gameId)
        {
            string? userId = userManager.GetUserId(User);

            if (userId == null)
            {
                return Challenge();
            }

            bool added = await libraryService.AddToLibraryAsync(gameId, userId);

            if (!added)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            string? userId = userManager.GetUserId(User);

            if (userId == null)
            {
                return Challenge();
            }

            await libraryService.RemoveFromLibraryAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string? userId = userManager.GetUserId(User);

            if (userId == null)
            {
                return Challenge();
            }

            var model = await libraryService.GetForEditAsync(id, userId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LibraryEditDto model)
        {
            string? userId = userManager.GetUserId(User);

            if (userId == null)
            {
                return Challenge();
            }

            if (!ModelState.IsValid)
            {
                var existingEntry = await libraryService.GetForEditAsync(model.Id, userId);

                if (existingEntry == null)
                {
                    return NotFound();
                }

                model.GameTitle = existingEntry.GameTitle;

                return View(model);
            }

            bool updated = await libraryService.EditAsync(model, userId);

            if (!updated)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}