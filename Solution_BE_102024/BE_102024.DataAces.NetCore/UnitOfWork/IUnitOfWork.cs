using BE102024.DataAces.NetCore.DAL_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE102024.DataAces.NetCore.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository _productRepository {  get; set; }
        IOrderRepository _orderRepository { get; set; }
        IOrderDetailRepository _orderDetailRepository { get; set; }

        Task<int> SaveChange();
    }
}
