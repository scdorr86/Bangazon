using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public int statusId { get; set; }
        public int productId { get; set; }

        public List<Product> Product { get; set; }
        public decimal orderTotal { get; set; }
        public int paymentType { get; set; }
    }
}
