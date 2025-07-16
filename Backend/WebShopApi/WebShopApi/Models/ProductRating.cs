namespace WebShopApi.Models
{
    public class ProductRating
    {
        public int Rate { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreationDate { get; set; }
        public int ProductId { get; set; }
    }
}
