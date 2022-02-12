using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FirstBackEndProj.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public Boolean Aviability { get; set; }
        public Byte Discount { get; set; }
        public Category Category { get; set; }
        public List<ProductSize> Sizes { get; set; }
        public List<ProductPhoto> Photos { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}