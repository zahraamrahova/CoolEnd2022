using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstBackEndProj.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Display(Name="Main Category")]
        public string Name { get; set; }
        public int ParentId { get; set; }

        public Category Parent { get; set; }

        public List<Product> Products { get; set; }
    }
}