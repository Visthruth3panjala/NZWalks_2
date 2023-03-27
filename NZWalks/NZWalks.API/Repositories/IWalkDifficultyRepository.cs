using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {

        Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficulties();

        Task<WalkDifficulty> GetWalkDifficultyById(Guid id);

        Task<WalkDifficulty> AddWalkDifficulty(WalkDifficulty walkDifficulty);

        Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDifficulty);

        Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id);

    }
}
