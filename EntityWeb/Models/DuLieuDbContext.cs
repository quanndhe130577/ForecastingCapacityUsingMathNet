using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Entity.Models
{
    public class DuLieuDbContext : DbContext
    {
         public DbSet<DuLieuDuDoan> DuDoanList { get; set; }
         public DbSet<DuLieuLichSu> LichSuList { get; set; }

         public System.Data.Entity.DbSet<Entity.Models.Account> AccountList { get; set; }
    }

}