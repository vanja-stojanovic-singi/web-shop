using WebShopApi.Models;

namespace WebShopApi.Repositories
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetBrands();
        Task<Brand> GetBrandById(int brandId);
        Task AddBrand(Brand brand);
        Task UpdateBrand(Brand brand);
        Task DeleteBrand(int brandId);
    }
}
