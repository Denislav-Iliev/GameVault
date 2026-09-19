using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GameVault.Common.EntityValidation;

namespace GameVault.ViewModels
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GameTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(GameDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Range(GameReleaseYearMin, GameReleaseYearMax)]
        public int ReleaseYear { get; set; }

        [Required]
        [MaxLength(GameDeveloperNameMaxLength)]
        public string Developer { get; set; } = null!;

        [MaxLength(ImageUrlMaxLength)]
        public string? ImageUrl { get; set; }

        [Required]
        public int GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        public virtual Genre Genre { get; set; } = null!;
    }
}
