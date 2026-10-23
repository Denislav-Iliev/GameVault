
using System.ComponentModel.DataAnnotations;
using static GameVault.GCommon.EntityValidation;

namespace GameVault.Services.Core.Models.Reviews
{
    public class ReviewFormDto
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        [Range(MinRating, MaxRating)]
        public int Rating { get; set; }

        [Required]
        [MaxLength(ReviewContentMaxLength)]
        public string Content { get; set; } = null!;
    }
}
