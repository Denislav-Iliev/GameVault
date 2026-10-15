
using GameVault.Data.Models;
using GameVault.Services.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameVault.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreService genreService;

        public GenresController(IGenreService genreService)
        {
            this.genreService = genreService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var genres = await genreService.GetAllAsync();

            return View(genres);
        }

    }
}
