using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstBackEndProj.Models
{
    public class AuthPermission
    {
        public int Id { get; set; }
        public int AuthGroupId { get; set; }
        public int AuthActionId { get; set; }
        public bool CanCreate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanEdit { get; set; }
        public bool CanView { get; set; }
        public bool OnlyOwner { get; set; }

        public AuthGroup AuthGroup { get; set; }
        public AuthAction AuthAction { get; set; }
    }
}