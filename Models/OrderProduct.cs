﻿using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}
