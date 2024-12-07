using BE_102024.DataAces.NetCore.CheckConditions;
using BE_102024.DataAces.NetCore.DataOpject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DAL
{
    public interface IProductRepositor
    {
        Task<ProductData> InsertProduct(Productt product);
        Task<ProductData> ImportProductFromExcel(string filePath);
        Task<List<Productt>> GetAllListProduct();
        Task<List<Productt>> SortProduct(int luaChon);
        Task<List<Productt>> GetProductByProductOfCategory(string? productName, string? CategoryName);
        Task DeleteProductById(int productId);
        Task<ProductData> ExportToExcel(string filePath);
    }
}
