using BE_102024.DataAces.NetCore.CheckConditions;
using BE102024.DataAces.NetCore.DataOpject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE102024.DataAces.NetCore.DAL_Interface
{
    public interface IOrderDetailRepository
    {
        Task<ProductData> InsertOrderDetail(InsertOrderDetail insertOrderDetail);
        Task<ProductData> UpdateOrderDetail(UpdateOrderDetail updateOrderDetail);
        Task<ProductData> DeleteOrderDetail(int OrderDetailId);
        Task<List<OrderDetail>> GetAllOrderDetails(string? ProductName, int? OrderDetailId);
    }
}
