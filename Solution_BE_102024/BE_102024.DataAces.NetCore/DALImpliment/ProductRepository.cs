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
    public class ProductRepository : IProductRepository
    {
        BE_102024Context _context;
        public ProductRepository(BE_102024Context context)
        {
            _context = context;
        }
        public async Task<ProductData> DeleteProduct(int ProductId)
        {
            var returnData = new ProductData();
            try
            {
                if (ProductId < 0) 
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "ProductId không hợp lệ";
                    return returnData;
                }
                var resultPro = _context.ProductOrder.FindAsync(ProductId);
                if (resultPro == null)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = $"Không tìm thấy: {ProductId}";
                    return returnData;
                }
                _context.Remove(resultPro);
            }
            catch (Exception ex)
            {
                returnData.ReponseCode = -99;
                returnData.ResponseMessenger = ex.Message;
            }
            return returnData;
        }

        public async Task<List<Product>> GetAllProducts(string? ProductName, int? ProductId)
        {
            try
            {
                var list = _context.ProductOrder.ToList();
                if (!string.IsNullOrEmpty(ProductName)) 
                {
                    list = list.FindAll(p => p.ProductName.Contains(ProductName));
                }
                if(ProductId.HasValue)
                {
                    list = list.Where(s => s.ProductId == ProductId).ToList();
                }
                return list;
            }
            catch(Exception ex)  
            {
                throw ex;
            }
        }

        public async Task<ProductData> InsertProduct(InsertProduct insertProduct)
        {
            var returnData = new ProductData();
            try
            {
                if (insertProduct == null) 
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "insertProduct NULL";
                    return returnData;
                }
                if (!Validation.CheckSting(insertProduct.ProductName))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.ProductNameNull;
                    returnData.ResponseMessenger = "ProductName NULL";
                    return returnData;
                }
                if (insertProduct.Price < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Price không hợp lệ";
                    return returnData;
                }
                if (insertProduct.Stock < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Stock không hợp lệ";
                    return returnData;
                }
                var result = new Product
                {
                    ProductName = insertProduct.ProductName,
                    Price = insertProduct.Price,
                    Stock = insertProduct.Stock,
                };
                _context.Add(result);
            }
            catch (Exception ex) 
            {
                returnData.ReponseCode = -99;
                returnData.ResponseMessenger = ex.Message;
            }
            return returnData;
        }

        public async Task<ProductData> UpdateProduct(UpdateProduct updateProduct)
        {
            var returnData = new ProductData();
            try
            {
                if (updateProduct == null)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "insertProduct NULL";
                    return returnData;
                }
                if (updateProduct.ProductId < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "ProductId không hợp lệ";
                    return returnData;
                }
                if (!Validation.CheckSting(updateProduct.ProductName))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.ProductNameNull;
                    returnData.ResponseMessenger = "ProductName NULL";
                    return returnData;
                }
                if (updateProduct.Price < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Price không hợp lệ";
                    return returnData;
                }
                if (updateProduct.Stock < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Stock không hợp lệ";
                    return returnData;
                }
                var result = new Product
                {
                    ProductId = updateProduct.ProductId,
                    ProductName = updateProduct.ProductName,
                    Price = updateProduct.Price,
                    Stock = updateProduct.Stock,
                };
                _context.Update(result);
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
