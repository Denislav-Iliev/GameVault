using System.ComponentModel.DataAnnotations;
using static GameVault.Common.EntityValidation;

namespace GameVault.ViewModels
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
