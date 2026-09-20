using GameVault.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GameVault.Common.EntityValidation;

namespace GameVault.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Range(MinRating, MaxRating)]
        public int Rating { get; set; }

        [Required]
        [MaxLength(ReviewContentMaxLength)]
        public string Content { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public int GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; } = null!;
    }
}