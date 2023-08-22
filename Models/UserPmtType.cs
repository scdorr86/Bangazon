using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models
{
    public class UserPmtType
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PaymentTypeId { get; set; }
    }
}
