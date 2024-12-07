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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        BE_102024Context _context;
        public OrderDetailRepository(BE_102024Context context)
        {
            _context = context;
        }
        public async Task<ProductData> DeleteOrderDetail(int OrderDetailId)
        {
            var returnData = new ProductData();
            try
            {
                if (OrderDetailId < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "OrderDetailId không hợp lệ";
                    return returnData;
                }
                var srfind = _context.OrderDetail.FindAsync(OrderDetailId);
                if (srfind == null)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = $"Không tìm thấy: {OrderDetailId}";
                    return returnData;
                }
                _context.Remove(srfind);
            }
            catch (Exception ex) 
            {
                returnData.ReponseCode = -99;
                returnData.ResponseMessenger = ex.Message;

            }
            return returnData;
        }

        public async Task<List<OrderDetail>> GetAllOrderDetails(string? ProductName, int? OrderDetailId)
        {
            try
            {
                var list = _context.OrderDetail.ToList();
                if (!string.IsNullOrEmpty(ProductName)) 
                {
                    list = list.FindAll(p => p.ProductName.Contains(ProductName));
                }
                if (OrderDetailId.HasValue)
                {
                    list =  list.Where(s => s.OrderDetailId == OrderDetailId).ToList();
                }
                return list;
            }
            catch(Exception ex)  
            {
                throw ex;
            }
        }

        public async Task<ProductData> InsertOrderDetail(InsertOrderDetail insertOrderDetail)
        {
            var returnData = new ProductData();

            try
            {
                if (insertOrderDetail == null)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "insertOrderDetail null";
                    return returnData;
                }
                if (insertOrderDetail.OrderDetailId < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "insertOrderDetail không hợp lệ";
                    return returnData;
                }
                if (insertOrderDetail.ProductId < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "ProductId không hợp lệ";
                    return returnData;
                }
                if (insertOrderDetail.OrderId < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "OrderId không hợp lệ";
                    return returnData;
                }
                if (insertOrderDetail.Quantity < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Quantity không hợp lệ";
                    return returnData;
                }
                if (insertOrderDetail.Subtotal < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Subtotal không hợp lệ";
                    return returnData;
                }
                if (insertOrderDetail.UnitPrice < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "UnitPrice không hợp lệ";
                    return returnData;
                }
                var result = new OrderDetail
                {
                    OrderDetailId = insertOrderDetail.OrderDetailId,
                    ProductId = insertOrderDetail.ProductId,
                    ProductName = insertOrderDetail.ProductName,
                    OrderId = insertOrderDetail.OrderId,
                    Quantity = insertOrderDetail.Quantity,
                    UnitPrice = insertOrderDetail.UnitPrice,
                    Subtotal = insertOrderDetail.Subtotal,
                };
                _context.Add(result);
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

        public async Task<ProductData> UpdateOrderDetail(UpdateOrderDetail updateOrderDetail)
        {
            var returnData = new ProductData();

            try
            {
                if (updateOrderDetail == null)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "insertOrderDetail null";
                    return returnData;
                }
                if (updateOrderDetail.OrderDetailId < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "insertOrderDetail không hợp lệ";
                    return returnData;
                }
                if (updateOrderDetail.ProductId < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "ProductId không hợp lệ";
                    return returnData;
                }
                if (updateOrderDetail.OrderId < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "OrderId không hợp lệ";
                    return returnData;
                }
                if (updateOrderDetail.Quantity < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Quantity không hợp lệ";
                    return returnData;
                }
                if (updateOrderDetail.Subtotal < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Subtotal không hợp lệ";
                    return returnData;
                }
                if (updateOrderDetail.UnitPrice < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "UnitPrice không hợp lệ";
                    return returnData;
                }
                var result = new OrderDetail
                {
                    OrderDetailId = updateOrderDetail.OrderDetailId,
                    ProductId = updateOrderDetail.ProductId,
                    ProductName = updateOrderDetail.ProductName,
                    OrderId = updateOrderDetail.OrderId,
                    Quantity = updateOrderDetail.Quantity,
                    UnitPrice = updateOrderDetail.UnitPrice,
                    Subtotal = updateOrderDetail.Subtotal,
                };
                _context.Add(result);
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
    }
}
