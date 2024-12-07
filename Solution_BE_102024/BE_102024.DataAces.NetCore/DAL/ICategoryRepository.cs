using BE_102024.DataAces.NetCore.DataOpject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DAL
{
    public interface ICategoryRepository
    {
        Task<bool> CheckCategoryExists(int categoryId);
        Task AddCategory(CategoryProduct categoryProduct);

        Task<List<CategoryProduct>> GetCategoryProductsByName(string CategoryName);
    }
}
