using WebShopApi.Models;

namespace WebShopApi.Repositories
{
    public interface IRatingRepository
    {
        Task<IEnumerable<Rating>> GetRatings();
        Task<Rating> GetRatingById(int ratingId);
        Task AddRating(Rating rating);
        Task UpdateRating(Rating rating);
        Task DeleteRating(int ratingId);
    }
}
