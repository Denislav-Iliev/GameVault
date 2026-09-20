namespace GameVault.Common
{
    public static class EntityValidation
    {
        public const int MaxGenreNameLength = 50;

        public const int GameTitleMaxLength = 100;

        public const int GameDescriptionMaxLength = 2000;

        public const int GameReleaseYearMin = 1950;

        public const int GameReleaseYearMax = 2026;

        public const int GameDeveloperNameMaxLength = 100;

        public const int ImageUrlMaxLength = 500;

        public const int MinRating = 1;

        public const int MaxRating = 10;

        public const int ReviewContentMaxLength = 1000;

        public const int UserGameStatusMaxLength = 20;

        public const int MinHoursPlayed = 0;

        public const int MaxHoursPlayed = 100000;

        public const int UserGameNotesMaxLength = 1000;
    }
}
