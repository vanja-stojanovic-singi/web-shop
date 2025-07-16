using System.Text.Json.Serialization;

namespace WebShopApi.Models
{
    public class ProductItem
    {
        public int Id { get; set; }
        public string Size { get; set; }

        [JsonIgnore]
        public virtual Product? Product { get; set; }
    }
}
