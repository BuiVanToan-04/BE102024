using BE_102024.DataAces.NetCore.DAL;
using BE_102024.DataAces.NetCore.DataOpject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DALImpliment
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly List<CategoryProduct> _listCategoryProducts = new List<CategoryProduct>();

        public async Task AddCategory(CategoryProduct categoryProduct)
        {
            _listCategoryProducts.Add(categoryProduct);
            await Task.CompletedTask;
            //await Task.CompletedTask; - là một cách hữu ích để thể hiện rằng một phương
            //thức bất đồng bộ đã hoàn thành mà không cần thực hiện bất kỳ công việc nào. Nó giúp duy trì
            //tính nhất quán trong mã và có thể cải thiện hiệu suất trong một số trường hợp.
        }
        public async Task<bool> CheckCategoryExists(int categoryId)
        {
            return await Task.Run(() =>
            {
                for (int i = 0;  i < _listCategoryProducts.Count; i++)
                {
                    if (categoryId == _listCategoryProducts[i].CategoryId)
                    {
                        return true;
                    }
                }
                return false;
            });
        }

        public async Task<List<CategoryProduct>> GetCategoryProductsByName(string categoryName)
        {
            return await Task.FromResult(
                _listCategoryProducts
                    .Where(p => p.CategoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase))
                    .ToList());
        }
    }
}
