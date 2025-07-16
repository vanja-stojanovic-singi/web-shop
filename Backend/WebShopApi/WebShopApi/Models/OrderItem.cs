using System.Text.Json.Serialization;

namespace WebShopApi.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public virtual Product? Product { get; set; }

        [JsonIgnore]
        public virtual Order? Order { get; set; }
    }
}
