using System.Text.Json.Serialization;

namespace WebShopApi.Models
{
    public class LoggedUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
