using System.Text.Json.Serialization;

namespace WebShopApi.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone {  get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip {  get; set; }

        [JsonIgnore]
        public IEnumerable<Order>? Orders { get; set; }

        [JsonIgnore]
        public IEnumerable<Rating>? Ratings { get; set; }
    }
}
