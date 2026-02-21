using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Repositories.Implementations
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _dbContext;
        public RegionRepository(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Region> AddAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid regionId)
        {
            var region = await _dbContext.Regions.FirstOrDefaultAsync(r => r.RegionId == regionId);
            if (region == null)
                return null;
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid regionId)
        {
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(r => r.RegionId == regionId);
            if (existingRegion == null)
                return null;

            _dbContext.Regions.Remove(existingRegion);
            await _dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?> UpdateAsync(Guid regionId, UpdateRegionRequestDto regionDto)
        {
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(r => r.RegionId == regionId);
            if (existingRegion == null)
                return null;

            existingRegion.Code = regionDto.Code;
            existingRegion.Name = regionDto.Name;
            existingRegion.RegionImageUrl = regionDto.RegionImageUrl;
            
            _dbContext.Regions.Update(existingRegion);
            await _dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
