using System.ComponentModel.DataAnnotations;
using static GameVault.GCommon.EntityValidation;
using GameVault.Data.Models;

namespace GameVault.ViewModels.Games
{
    public class GameFormViewModel
    {
        [Required(ErrorMessage = "The title cannot be empty!")]
        [StringLength(GameTitleMaxLength, MinimumLength = GameTitleMinLength, ErrorMessage = "Title must be between {2} and {1} characters long!")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "The description cannot be empty!")]
        [StringLength(GameDescriptionMaxLength, MinimumLength = GameDescriptionMinLength, ErrorMessage = "Description must be between {2} and {1} characters long!")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Date is required!")]
        [Range(GameReleaseYearMin, GameReleaseYearMax, ErrorMessage = "Release year must be between {1} and {2} long!")]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Developer name cannot be empty!")]
        [StringLength(GameDeveloperNameMaxLength, MinimumLength = GameDeveloperMinLength, ErrorMessage = "Developer name must be between {2} and {1} characters long!")]
        public string Developer { get; set; } = null!;

        [MaxLength(ImageUrlMaxLength)]
        public string? ImageUrl { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a genre.")]
        public int GenreId { get; set; }

        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();
    }
}
