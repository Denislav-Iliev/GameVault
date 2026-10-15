using GameVault.Services.Core.Models.Genres;
using GameVault.Data.Models;
using GameVault.Services.Core.Contracts;
using GameVault.Services.Core.Models.Games;
using Microsoft.AspNetCore.Mvc;

namespace GameVault.Controllers
{
    public class GamesController : Controller
    {
        private const string InvalidRequestMessage =
            "There was an error with your request! Please try again!";

        private readonly IGameService gameService;

        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(
            [FromQuery] int? genreId,
            [FromQuery] string? searchTerm)
        {
            IEnumerable<GameListDto> games =
                await gameService.GetAllAsync(genreId, searchTerm);

            ViewBag.GenreId = genreId;
            ViewBag.Genres = await LoadAllGenresAsync();

            return View(games);
        }



        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            GameDetailsDto? game =
                await gameService.GetDetailsAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            GameFormViewModel model = new GameFormViewModel
            {
                Genres = await LoadAllGenresAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Invalid data was detected!");

                model.Genres = await LoadAllGenresAsync();
                return View(model);
            }

            if (model.ReleaseYear > DateTime.Today.Year)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Invalid release year was detected!");

                model.Genres = await LoadAllGenresAsync();
                return View(model);
            }

            bool genreExists =
                await gameService.GenreExistsAsync(model.GenreId);

            if (!genreExists)
            {
                ModelState.AddModelError(
                    nameof(model.GenreId),
                    "Invalid genre was selected!");

                model.Genres = await LoadAllGenresAsync();
                return View(model);
            }

            GameFormDto dto = ToFormDto(model);

            await gameService.CreateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int? id)
        {
            if (!id.HasValue || id.Value <= 0)
            {
                return BadRequest(InvalidRequestMessage);
            }

            GameFormDto? gameDto =
                await gameService.GetForEditAsync(id.Value);

            if (gameDto == null)
            {
                return NotFound();
            }

            GameFormViewModel model = new GameFormViewModel
            {
                Title = gameDto.Title,
                Description = gameDto.Description,
                ReleaseYear = gameDto.ReleaseYear,
                Developer = gameDto.Developer,
                ImageUrl = gameDto.ImageUrl,
                GenreId = gameDto.GenreId,
                Genres = await LoadAllGenresAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            [FromRoute] int id,
            GameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Invalid data was detected!");

                model.Genres = await LoadAllGenresAsync();
                return View(model);
            }

            if (model.ReleaseYear > DateTime.Today.Year)
            {
                ModelState.AddModelError(
                    nameof(model.ReleaseYear),
                    "Invalid release year was detected!");

                model.Genres = await LoadAllGenresAsync();
                return View(model);
            }

            bool genreExists =
                await gameService.GenreExistsAsync(model.GenreId);

            if (!genreExists)
            {
                ModelState.AddModelError(
                    nameof(model.GenreId),
                    "Invalid genre was selected!");

                model.Genres = await LoadAllGenresAsync();
                return View(model);
            }

            GameFormDto dto = ToFormDto(model);

            bool edited = await gameService.EditAsync(id, dto);

            if (!edited)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            if (!id.HasValue || id.Value <= 0)
            {
                return BadRequest(InvalidRequestMessage);
            }

            GameDeleteDto? gameDto =
                await gameService.GetForDeleteAsync(id.Value);

            if (gameDto == null)
            {
                return NotFound();
            }

            GameDeleteViewModel model = new GameDeleteViewModel
            {
                Id = gameDto.Id,
                Title = gameDto.Title
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest(InvalidRequestMessage);
            }

            bool deleted = await gameService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<GenreDto>> LoadAllGenresAsync()
        {
            return await gameService.GetGenresAsync();
        }

        private static GameFormDto ToFormDto(GameFormViewModel model)
        {
            return new GameFormDto
            {
                Title = model.Title,
                Description = model.Description,
                ReleaseYear = model.ReleaseYear,
                Developer = model.Developer,
                ImageUrl = model.ImageUrl,
                GenreId = model.GenreId
            };
        }
    }
}
