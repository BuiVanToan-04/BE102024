using BE_102024.DataAces.NetCore.CheckConditions;
using BE102024.DataAces.NetCore.DataOpject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE102024.DataAces.NetCore.DAL_Interface
{
    public interface IOrderRepository
    {
        Task<ProductData> InsertOrder(InsertOrder insertOrder);
        Task<ProductData> UpdateOrder(UpdateOrder updateOrder);
        Task<ProductData> DeleteOrder(int OrderId);
        Task<List<Order>> GetAllOrders(string? CustomerName, int? OrderId);
    }
}
