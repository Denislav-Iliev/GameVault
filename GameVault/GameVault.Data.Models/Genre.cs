using System.ComponentModel.DataAnnotations;
using static GameVault.GCommon.EntityValidation;

namespace GameVault.Data.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxGenreNameLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Game> Games { get; set; }
            = new List<Game>();
    }
}
