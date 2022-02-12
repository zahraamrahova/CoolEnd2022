using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstBackEndProj.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }

        public List<Product> Products { get; set; }
    }
}