using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE102024.DataAces.NetCore.DataOpject
{
    public class CreateOrderRequest
    {
        public InsertProduct product { get; set; }
        public InsertOrder order { get; set; }
        public InsertOrderDetail orderDetail { get; set; }
    }
}
