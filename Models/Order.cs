using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public int statusId { get; set; }
        public List<Product> Products { get; } = new();
        [NotMapped]
        public decimal orderTotal => Products.Sum(p => p.ProductPrice);
        public int paymentType { get; set; }
    }
}
