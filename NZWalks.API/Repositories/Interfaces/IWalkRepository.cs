using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAll();

        Task<Walk?> GetById(Guid walkId);

        Task<Walk?> UpdateAsync(Guid walkId, Walk walk);

        Task<Walk?> DeleteAsync(Guid walkId);
    }
}
