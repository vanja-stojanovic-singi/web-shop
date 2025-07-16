using Microsoft.EntityFrameworkCore;
using WebShopApi.Database;
using WebShopApi.Models;

namespace WebShopApi.Repositories.Implementation
{
    public class RatingRepository : IRatingRepository
    {
        private readonly AppDbContext _dbContext;

        public RatingRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Rating>> GetRatings()
        {
            return await _dbContext.Ratings.ToListAsync();
        }

        public async Task<Rating> GetRatingById(int ratingId)
        {
            return await _dbContext.Ratings.FirstOrDefaultAsync(r => r.Id == ratingId);
        }

        public async Task AddRating(Rating rating)
        {
            await _dbContext.Ratings.AddAsync(rating);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRating(int ratingId)
        {
            var rating = await _dbContext.Ratings.FirstOrDefaultAsync(r => r.Id == ratingId);

            if (rating != null)
            {
                _dbContext.Ratings.Remove(rating);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateRating(Rating rating)
        {
            _dbContext.Ratings.Update(rating);
            await _dbContext.SaveChangesAsync();
        }
    }
}
