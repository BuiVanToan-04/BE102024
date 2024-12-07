
using BE_102024.DataAces.NetCore.CheckConditions;
using BE_102024.DataAces.NetCore.DBContext;
using BE102024.DataAces.NetCore.DAL_Interface;
using BE102024.DataAces.NetCore.DataOpject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE102024.DataAces.NetCore.DAL_Impliment
{
    public class OrderRepository : IOrderRepository
    {
        BE_102024Context _context;
        public OrderRepository(BE_102024Context context)
        {
            _context = context;
        }

        public async Task<ProductData> DeleteOrder(int OrderId)
        {
            var returnData = new ProductData();
            try
            {
                if (OrderId < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "OrderId không hợp lệ";
                    return returnData;
                }
                var resultOrder = _context.ProductOrder.FindAsync(OrderId);
                if (resultOrder == null)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = $"Không tìm thấy: {OrderId}";
                    return returnData;
                }
                _context.Remove(resultOrder);
            }
            catch (Exception ex)
            {
                returnData.ReponseCode = -99;
                returnData.ResponseMessenger = ex.Message;
            }
            return returnData;
        }

        public async Task<List<Order>> GetAllOrders(string? CustomerName, int? OrderId)
        {
            try
            {
                var list = _context.Order.ToList();
                if (!string.IsNullOrEmpty(CustomerName))
                {
                    list = list.FindAll(p => p.CustomerName.Contains(CustomerName));
                }
                if (OrderId.HasValue)
                {
                    list = list.Where(s => s.OrderId == OrderId).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductData> InsertOrder(InsertOrder insertOrder)
        {
            var returnData = new ProductData();
            try
            {
                if (insertOrder == null)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "insertOrder null";
                    return returnData;
                }
                if (insertOrder.OrderId < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "insertOrder không hợp lệ";
                    return returnData;
                }
                if (insertOrder.Price < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Price không hợp lệ";
                    return returnData;
                }
                if (!Validation.CheckSting(insertOrder.CustomerName))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.ProductNameNull;
                    returnData.ResponseMessenger = "CustomerName null";
                    return returnData;
                }
                if (!Validation.CheckXSSInput(insertOrder.CustomerName))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.InvalidXSSInput;
                    returnData.ResponseMessenger = "CustomerName lỗi Xss";
                    return returnData;
                }
                if (!Validation.CheckSting(insertOrder.Status))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.ProductNameNull;
                    returnData.ResponseMessenger = "Status null";
                    return returnData;
                }
                var resultOrder = new Order
                {
                    OrderId = insertOrder.OrderId,
                    OrderDate = DateTime.Now,
                    CustomerName = insertOrder.CustomerName,
                    Price = insertOrder.Price,
                    Status = insertOrder.Status,
                };
                _context.Add(resultOrder);
                //var rs = _context.SaveChanges();
                //if (rs == 0)
                //{
                //    returnData.ReponseCode = -5;
                //    returnData.ResponseMessenger = "Thêm mới thất bại";
                //    return returnData;
                //}
                //returnData.ReponseCode = (int)ProductInsertStatus.Success;
                //returnData.ResponseMessenger = "Thêm mới thành công";
                //return returnData;
            }
            catch (Exception ex)
            {
                returnData.ReponseCode = -99;
                returnData.ResponseMessenger = ex.Message;
            }
            return returnData;
        }

        public async Task<ProductData> UpdateOrder(UpdateOrder updateOrder)
        {
            var returnData = new ProductData();
            try
            {
                if (updateOrder == null)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "insertOrder null";
                    return returnData;
                }
                if (updateOrder.OrderId < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "insertOrder không hợp lệ";
                    return returnData;
                }
                if (updateOrder.Price < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "TotalAmount không hợp lệ";
                    return returnData;
                }
                if (!Validation.CheckSting(updateOrder.CustomerName))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.ProductNameNull;
                    returnData.ResponseMessenger = "CustomerName null";
                    return returnData;
                }
                if (!Validation.CheckXSSInput(updateOrder.CustomerName))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.InvalidXSSInput;
                    returnData.ResponseMessenger = "CustomerName lỗi Xss";
                    return returnData;
                }
                if (!Validation.CheckSting(updateOrder.Status))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.ProductNameNull;
                    returnData.ResponseMessenger = "Status null";
                    return returnData;
                }
                var resultOrder = new Order
                {
                    OrderId = updateOrder.OrderId,
                    OrderDate = DateTime.Now,
                    CustomerName = updateOrder.CustomerName,
                    Price = updateOrder.Price,
                    Status = updateOrder.Status,
                };
                _context.Update(resultOrder);
            }
            catch (Exception ex)
            {
                returnData.ReponseCode = -99;
                returnData.ResponseMessenger = ex.Message;
            }
            return returnData;
        }
    }
}
