using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstBackEndProj.Models
{
    public class ProductSize
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}