namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid WalkId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        public Difficulty Difficulty { get; set; } = default!;
        public Region Region { get; set; } = default!;
    }
}
