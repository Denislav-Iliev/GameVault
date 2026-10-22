using System.ComponentModel.DataAnnotations;
using static GameVault.GCommon.EntityValidation;


namespace GameVault.Services.Core.Models.Library
{
    public class LibraryEditDto
    {
        public int Id { get; set; }

        public string? GameTitle { get; set; } = null!;

        [Required]
        [MaxLength(UserGameStatusMaxLength)]
        public string Status { get; set; } = null!;

        [Range(MinHoursPlayed, MaxHoursPlayed)]
        public int HoursPlayed { get; set; }

        [MaxLength(UserGameNotesMaxLength)]
        public string? Notes { get; set; }
    }
}