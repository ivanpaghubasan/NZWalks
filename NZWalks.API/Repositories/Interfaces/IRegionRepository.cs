using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid regionId);
        Task<Region> AddAsync(Region region);
        Task<Region?> UpdateAsync(Guid regionId, UpdateRegionRequestDto regionDto);
        Task<Region?> DeleteAsync(Guid regionId);
    }
}
