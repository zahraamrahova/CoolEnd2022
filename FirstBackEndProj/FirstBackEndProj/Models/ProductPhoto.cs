using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstBackEndProj.Models
{
    public class ProductPhoto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Name { get; set; }
    }
}