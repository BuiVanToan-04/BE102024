using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE102024.DataAces.NetCore.DataOpject
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public string? Status { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
    public class InsertOrder
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }
    public class UpdateOrder
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}
