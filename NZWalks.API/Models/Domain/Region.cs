namespace NZWalks.API.Models.Domain
{
    public class Region
    {
        public Guid RegionId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? RegionImageUrl { get; set; }
    }
}
