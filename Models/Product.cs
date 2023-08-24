using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int productType { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int userId { get; set; }
        public List<Order> Orders { get; } = new();
        public List<OrderProduct> OrderProducts { get; set; }
    }
}
