using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FirstBackEndProj.Models
{
    public enum Status
    {
        Approvied,
        Denied,
        Delivered
    }
    public class Order
    {
        
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Status Status { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public Client Client { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}