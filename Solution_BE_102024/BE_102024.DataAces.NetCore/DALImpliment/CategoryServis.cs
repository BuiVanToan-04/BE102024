using BE_102024.DataAces.NetCore.CheckConditions;
using BE_102024.DataAces.NetCore.DAL;
using BE_102024.DataAces.NetCore.DataOpject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DALImpliment
{
    public class CategoryServis : ICategoryServis
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryServis(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ProductData> InsertCategory(CategoryProduct categoryProduct)
        {
            var returnData = new ProductData();
            try
            {
                // Kiểm tra CategoryID
                if (categoryProduct.CategoryId < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Dữ liệu CategoryProductID không hợp lệ";
                    return returnData;
                }

                // Kiểm tra CategoryName
                if (!Validation.CheckSting(categoryProduct.CategoryName))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Dữ liệu CategoryName không hợp lệ";
                    return returnData;
                }

                // Kiểm tra XSS
                if (!Validation.CheckXSSInput(categoryProduct.CategoryName))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.InvalidXSSInput;
                    returnData.ResponseMessenger = "Dữ liệu CategoryName lỗi XSS";
                    return returnData;
                }

                await _categoryRepository.AddCategory(categoryProduct); 
                returnData.ReponseCode = (int)ProductInsertStatus.Success;
                returnData.ResponseMessenger = "Thêm CategoryProduct thành công";
                return returnData; 
            }
            catch (Exception ex)
            {
                returnData.ReponseCode = -99;
                returnData.ResponseMessenger = ex.Message;
                return returnData;
            }
        }
    }
}
