namespace WebShopApi.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int Rate {  get; set; }
        public virtual User Customer { get; set; }
        public virtual OrderItem OrderItem { get; set; }
    }
}
