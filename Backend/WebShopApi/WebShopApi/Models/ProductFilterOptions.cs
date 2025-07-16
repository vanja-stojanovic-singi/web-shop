namespace WebShopApi.Models
{
    public class ProductFilterOptions
    {
        public IEnumerable<Brand>? Brands { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
    }
}
