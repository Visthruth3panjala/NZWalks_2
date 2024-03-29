﻿using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDifficulty> AddWalkDifficulty(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDbContext.WalksDifficulty.AddAsync(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id)
        {
            var existingWalkDifficulty = await nZWalksDbContext.WalksDifficulty.FindAsync(id);

            if (existingWalkDifficulty != null)
            {
                nZWalksDbContext.WalksDifficulty.Remove(existingWalkDifficulty);
                await nZWalksDbContext.SaveChangesAsync() ;
                return existingWalkDifficulty;
            }

            return null;

        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficulties()
        {
            return await nZWalksDbContext.WalksDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetWalkDifficultyById(Guid id)
        {
            return await nZWalksDbContext.WalksDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await nZWalksDbContext.WalksDifficulty.FindAsync(id);

            if (existingWalkDifficulty == null)
            {
                return null;
            }

            existingWalkDifficulty.Code = walkDifficulty.Code;
            await nZWalksDbContext.SaveChangesAsync();
            return existingWalkDifficulty;  
        }
    }
}
