using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalkAsync();

        Task<Walk> GetWalkAsync(Guid id);

        Task<Walk> AddWalkAsync(Walk walk);

        Task<Walk> UpdateWalkAsync(Guid id,  Walk walk);
        
        Task<Walk> DeleteWalkAsync(Guid id);

    }
}
