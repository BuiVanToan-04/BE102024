using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE102024.DataAces.NetCore.DataOpject
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
    public class InsertProduct
    {
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
    public class UpdateProduct
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
