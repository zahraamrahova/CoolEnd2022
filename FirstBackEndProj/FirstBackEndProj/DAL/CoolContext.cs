using FirstBackEndProj.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FirstBackEndProj.DAL
{
    public class CoolContext : DbContext
    {
        public CoolContext() : base("CoolConnnection")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<AuthAction> AuthActions { get; set; }
        public DbSet<AuthGroup> AuthGroups { get; set; }
        public DbSet<AuthPermission> AuthPermissions { get; set; }
    }
}