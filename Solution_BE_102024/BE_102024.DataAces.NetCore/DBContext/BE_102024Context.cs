using BE_102024.DataAces.NetCore.DataOpject;
using BE102024.DataAces.NetCore.DataOpject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DBContext
{
    public class BE_102024Context : Microsoft.EntityFrameworkCore.DbContext 
    {
        public BE_102024Context(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public virtual DbSet<Room> room {  get; set; }

        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Product> ProductOrder { get; set; }
    }
}
