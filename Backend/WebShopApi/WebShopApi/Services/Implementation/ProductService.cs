using WebShopApi.Models;
using WebShopApi.Repositories;

namespace WebShopApi.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository repository, IBrandRepository brandRepository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _repository.GetProducts();
        }

        public async Task<IEnumerable<Product>> GetProductsByFilters(string search, IEnumerable<int> brandIds, IEnumerable<int> categoryIds, IEnumerable<string> sizes, IEnumerable<string> categoryNames)
        {
            return await _repository.GetProductsByFilters(search, brandIds, categoryIds, sizes, categoryNames);
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _repository.GetProductById(productId);
        }

        public async Task<Product> AddProduct(Product product)
        {
            return await _repository.AddProduct(product);
        }

        public async Task DeleteProduct(int productId)
        {
            await _repository.DeleteProduct(productId);
        }

        public async Task UpdateProduct(Product product)
        {
            await _repository.UpdateProduct(product);
        }

        public async Task<ProductFilterOptions> GetProductFilterOptions()
        {
            var brands = await _brandRepository.GetBrands();
            var categories = await _categoryRepository.GetCategories();

            return new ProductFilterOptions
            {
                Brands = brands,
                Categories = categories,
            };
        }

        public async Task<IEnumerable<ProductRating>> GetProductRatings(int productId)
        {
            return await _repository.GetProductRatings(productId);
        }
    }
}
