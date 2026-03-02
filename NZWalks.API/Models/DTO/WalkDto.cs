namespace NZWalks.API.Models.DTO
{
    public class WalkDto
    {
        public Guid WalkId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public RegionDto Region { get; set; } = default!;
        public DifficultyDto Difficulty { get; set; } = default!;
    }
}
