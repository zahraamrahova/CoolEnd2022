using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstBackEndProj.Models
{
    public class AuthGroup
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public List<User> Users { get; set; }

        public List<AuthPermission> AuthPermissions { get; set; }
    }
}