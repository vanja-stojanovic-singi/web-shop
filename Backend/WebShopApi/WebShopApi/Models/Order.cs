namespace WebShopApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual User Customer { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; }
    }
}
