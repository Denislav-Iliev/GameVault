using GameVault.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Data
{
    public class GameVaultDbContext : IdentityDbContext<ApplicationUser>
    {
        public GameVaultDbContext(
            DbContextOptions<GameVaultDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Games { get; set; } = null!;

        public virtual DbSet<Genre> Genres { get; set; } = null!;

        public virtual DbSet<Review> Reviews { get; set; } = null!;

        public virtual DbSet<UserGame> UserGames { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(GameVaultDbContext).Assembly);
        }
    }
}