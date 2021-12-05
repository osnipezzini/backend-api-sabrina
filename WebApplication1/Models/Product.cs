namespace WebApplication1.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
