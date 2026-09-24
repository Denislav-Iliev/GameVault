using GameVault.Models;
using GameVault.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static System.Net.WebRequestMethods;

namespace GameVault.Data.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasData(
                new Game
                {
                    Id = 1,
                    Title = "The Witcher 3: Wild Hunt",
                    Description = "An open-world RPG following Geralt of Rivia on his search for Ciri.",
                    ReleaseYear = 2015,
                    Developer = "CD Projekt Red",
                    ImageUrl = "/images/games/Witcher3.png",
                    GenreId = 3
                },
                new Game
                {
                    Id = 2,
                    Title = "Diablo II: Resurrected",
                    Description = "A remastered action RPG focused on character progression, loot and challenging combat.",
                    ReleaseYear = 2021,
                    Developer = "Blizzard Entertainment",
                    ImageUrl = "/images/games/D2R.png",
                    GenreId = 3
                },
                new Game
                {
                    Id = 3,
                    Title = "Elden Ring",
                    Description = "An open-world action RPG set in the mysterious Lands Between.",
                    ReleaseYear = 2022,
                    Developer = "FromSoftware",
                    ImageUrl = "/images/games/EldenRing.webp",
                    GenreId = 3
                },
                new Game
                {
                    Id = 4,
                    Title = "Counter-Strike 2",
                    Description = "A competitive tactical first-person shooter focused on team-based matches.",
                    ReleaseYear = 2023,
                    Developer = "Valve",
                    ImageUrl = "/images/games/CS2.jpg",
                    GenreId = 5
                },
                new Game
                {
                    Id = 5,
                    Title = "DOOM Eternal",
                    Description = "A fast-paced first-person shooter featuring intense combat against demonic forces.",
                    ReleaseYear = 2020,
                    Developer = "id Software",
                    ImageUrl = "/images/games/DoomEternal.jpg",
                    GenreId = 5
                },
                new Game
                {
                    Id = 6,
                    Title = "Civilization VI",
                    Description = "A turn-based strategy game about building and developing a civilization.",
                    ReleaseYear = 2016,
                    Developer = "Firaxis Games",
                    ImageUrl = "/images/games/CivilizationVI.jpg",
                    GenreId = 4
                },
                new Game
                {
                    Id = 7,
                    Title = "Resident Evil 4",
                    Description = "A survival horror game following Leon Kennedy on a dangerous rescue mission.",
                    ReleaseYear = 2023,
                    Developer = "Capcom",
                    ImageUrl = "/images/games/RE4.jpg",
                    GenreId = 8
                },
                new Game
                {
                    Id = 8,
                    Title = "Microsoft Flight Simulator",
                    Description = "A flight simulation game featuring detailed aircraft and locations around the world.",
                    ReleaseYear = 2020,
                    Developer = "Asobo Studio",
                    ImageUrl = "/images/games/MFS2024.jpg",
                    GenreId = 6
                },
                new Game
                {
                    Id = 9,
                    Title = "EA Sports FC 25",
                    Description = "A football simulation game featuring clubs, players and competitions from around the world.",
                    ReleaseYear = 2024,
                    Developer = "EA Sports",
                    ImageUrl = "/images/games/FC25.jpg",
                    GenreId = 7
                },
                new Game
                {
                    Id = 10,
                    Title = "Red Dead Redemption 2",
                    Description = "An open-world adventure following outlaw Arthur Morgan and the Van der Linde gang.",
                    ReleaseYear = 2018,
                    Developer = "Rockstar Games",
                    ImageUrl = "/images/games/RDR2.webp",
                    GenreId = 2
                },
                new Game
                {
                    Id = 11,
                    Title = "God of War",
                    Description = "Kratos and his son Atreus journey through the Norse realms while facing gods, monsters and their own past.",
                    ReleaseYear = 2018,
                    Developer = "Santa Monica Studio",
                    ImageUrl = "/images/games/god-of-war.jpg",
                    GenreId = 1
                }
            );
        }
    }
}