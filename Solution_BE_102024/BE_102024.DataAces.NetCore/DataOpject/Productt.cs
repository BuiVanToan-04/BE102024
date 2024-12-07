using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DataOpject
{
    public class Productt
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }

        public double? ProductPrice { get; set; }
        public int CategoryId { get; set; }

        public Productt() { }
       
        public Productt(int ProductId, string? ProductName, double? ProductPrice, int CategoryId)
        {
            this.ProductId = ProductId;
            this.ProductName = ProductName;
            this.ProductPrice = ProductPrice;
            this.CategoryId = CategoryId;
        }
    }
}
