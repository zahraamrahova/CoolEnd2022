using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstBackEndProj.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public int AuthGroupId { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(200)]
        public string PasswordHash { get; set; }
        [StringLength(200)]
        public string Token { get; set; }
        [Required]
        public bool Status { get; set; }
        public AuthGroup AuthGroup { get; set; }
    }
}