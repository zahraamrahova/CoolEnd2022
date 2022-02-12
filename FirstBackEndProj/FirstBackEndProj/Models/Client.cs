using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstBackEndProj.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Fullname { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(250)]
        public string Adress { get; set; }
        [Required]
        [StringLength(15)]
        public string Phone { get; set; }

        public List<Order> Orders { get; set; }
    }
}