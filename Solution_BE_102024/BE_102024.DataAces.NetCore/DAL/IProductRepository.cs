
using BE_102024.DataAces.NetCore.CheckConditions;
using BE102024.DataAces.NetCore.DataOpject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE102024.DataAces.NetCore.DAL_Interface
{
    public interface IProductRepository
    {
        Task<ProductData> InsertProduct(InsertProduct insertProduct);
        Task<ProductData> UpdateProduct(UpdateProduct updateProduct);
        Task<ProductData> DeleteProduct(int ProductId);
        Task<List<Product>> GetAllProducts(string? ProductName, int? ProductId);
    }
}
