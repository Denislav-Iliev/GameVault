using GameVault.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameVault.Data.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Adventure" },
                new Genre { Id = 3, Name = "RPG" },
                new Genre { Id = 4, Name = "Strategy" },
                new Genre { Id = 5, Name = "Shooter" },
                new Genre { Id = 6, Name = "Simulation" },
                new Genre { Id = 7, Name = "Sports" },
                new Genre { Id = 8, Name = "Horror" }
            );
        }
    }
}
