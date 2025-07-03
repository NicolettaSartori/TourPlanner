using System;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace TourPlanner.DataAccessLayer.Repositories
{
    public class TourLogRepository
    {
        private readonly AppdDbContext _dbContext;

        public TourLogRepository(AppdDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adds a new tour log entry for a specific tour.
        /// </summary>
        public async Task AddTourLogAsync(TourLog log, Guid tourId)
        {
            log.Id = Guid.NewGuid();
            log.TourId = tourId;
            log.DateTime = log.DateTime.ToUniversalTime();

            await _dbContext.TourLogs.AddAsync(log);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing tour log.
        /// </summary>
        public async Task UpdateTourLogAsync(TourLog log)
        {
            var existingLog = await _dbContext.TourLogs.FirstOrDefaultAsync(l => l.Id == log.Id);
            if (existingLog == null)
                return;

            // Felder updaten – ACHTUNG: DateTime auf UTC setzen!
            existingLog.DateTime = log.DateTime.ToUniversalTime(); // <- WICHTIG
            existingLog.Comment = log.Comment;
            existingLog.Difficulty = log.Difficulty;
            existingLog.TotalDistance = log.TotalDistance;
            existingLog.TotalTime = log.TotalTime;
            existingLog.Rating = log.Rating;
            existingLog.TourId = log.TourId;

            await _dbContext.SaveChangesAsync();
        }

        
        public async Task<List<TourLog>> GetLogsForTourAsync(Guid tourId)
        {
            return await _dbContext.TourLogs
                .Where(log => log.TourId == tourId)
                .ToListAsync();
        }


        /// <summary>
        /// Deletes a tour log by its ID.
        /// </summary>
        public async Task DeleteTourLogAsync(Guid logId)
        {
            var log = await _dbContext.TourLogs.FindAsync(logId);
            if (log != null)
            {
                _dbContext.TourLogs.Remove(log);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}