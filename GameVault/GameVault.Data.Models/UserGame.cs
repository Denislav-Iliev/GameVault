using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GameVault.GCommon.EntityValidation;

namespace GameVault.Data.Models
{
    public class UserGame
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(UserGameStatusMaxLength)]
        public string Status { get; set; } = null!;

        [Range(MinHoursPlayed, MaxHoursPlayed)]
        public int HoursPlayed { get; set; }

        [MaxLength(UserGameNotesMaxLength)]
        public string? Notes { get; set; }

        public DateTime AddedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public int GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; } = null!;
    }
}