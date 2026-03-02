using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Repositories.Implementations
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await nZWalksDbContext.Walks.AddAsync(walk);

            await nZWalksDbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid walkId)
        {
            var existingWalk = await GetById(walkId);
            if (existingWalk == null)
            {
                return null;
            }

            nZWalksDbContext.Remove(existingWalk);
            await nZWalksDbContext.SaveChangesAsync();

            return existingWalk;
        }

        public async Task<List<Walk>> GetAll()
        {
            var walks = await nZWalksDbContext.Walks
                .Include(w => w.Difficulty)
                .Include(w => w.Region)
                .ToListAsync();
            return walks;
        }

        public async Task<Walk?> GetById(Guid walkId)
        {
            var walk = await nZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.Difficulty)
                .FirstOrDefaultAsync(x => x.WalkId == walkId);

            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid walkId, Walk walk)
        {
            var existingWalk = await GetById(walkId);
            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            nZWalksDbContext.Update(existingWalk);
            await nZWalksDbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
