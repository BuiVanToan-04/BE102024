using BE_102024.DataAces.NetCore.CheckConditions;
using BE_102024.DataAces.NetCore.DAL;
using BE_102024.DataAces.NetCore.DataOpject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace BE_102024.DataAces.NetCore.DALImpliment
{
    public class ProductRepositor : IProductRepositor
    {
        private readonly ICategoryRepository _categoryRepository;
        private static List<Productt> _listProducts;
        public ProductRepositor(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _listProducts = new List<Productt>();
        }

        public async Task<ProductData> InsertProduct(Productt product)
        {
            var returnData = new ProductData();

            try
            {
                //Kiểm tra ProductID
                if (product.ProductId < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Dữ liệu ID không hợp lệ";
                    return returnData;
                }

                //Kiểm tra ProductName 
                if (!Validation.CheckSting(product.ProductName))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.ProductNameNull;
                    returnData.ResponseMessenger = "Dữ liệu ProductName không hợp lệ";
                    return returnData;
                }

                //CheckXSS ProductName
                if (!Validation.CheckXSSInput(product.ProductName))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.InvalidXSSInput;
                    returnData.ResponseMessenger = "Dữ liệu ProductName lỗi XSS";
                    return returnData;
                }

                //Check Price
                if (product.ProductPrice < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Dữ liệu ProductPrice không hợp lệ";
                    return returnData;
                }

                //Check CategoryId
                if (product.CategoryId < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Dữ liệu CategoryId không hợp lệ";
                    return returnData;
                }

                //Kiểm tra CategoryId tồn tại
                var checkCategoryId = await _categoryRepository.CheckCategoryExists(product.CategoryId);
                if (!checkCategoryId)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "CategoryId Không tồn tại";
                    return returnData;
                }
                _listProducts.Add(product);
                returnData.ReponseCode = (int)ProductInsertStatus.Success;
                returnData.ResponseMessenger = "Thêm sản phẩm thành công";
                return returnData;
            }
            catch (Exception ex)
            {
                returnData.ReponseCode = -99;
                returnData.ResponseMessenger = ex.Message;
                return returnData;
            }
        }



        public async Task<List<Productt>> GetAllListProduct()
        {
            return await Task.FromResult(_listProducts.ToList());
        }

        public async Task<List<Productt>> GetProductByProductOfCategory(string? productName, string? categoryName)
        {
            var result = _listProducts
                .Where(product =>
                {
                    bool resultName = string.IsNullOrEmpty(productName)
                        || product.ProductName.Contains(productName, StringComparison.OrdinalIgnoreCase);

                    // - string.IsNullOrEmpty(productName) - Kiểm tra productName có null hay không?
                    // - product.ProductName.Contains(productName, - Kiểm tra tên sản phẩm có chứa chuỗi
                    //con(tên sản phẩm đc truyền vào) hay không
                    // - StringComparison.OrdinalIgnoreCase); - So sánh Không phân biệt in hoa hay thường

                    bool resultCategoryName = string.IsNullOrEmpty(categoryName)
                        || _categoryRepository.GetCategoryProductsByName(categoryName).Result.Any();

                    return resultName && resultCategoryName;
                })
                .ToList();

            return await Task.FromResult(result);
        }

        public Task DeleteProductById(int productId)
        {

            var product = _listProducts.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                _listProducts.Remove(product);
            }
            return Task.CompletedTask;
        }

        public async Task<ProductData> ImportProductFromExcel(string filePath)
        {
            var returnData = new ProductData();
            try
            {
                using (var exProduct = new XLWorkbook(filePath))
                {
                    var workSheet = exProduct.Worksheet(1);
                    int rowCount = workSheet.RowsUsed().Count();
                    for (int i = 2; i < rowCount; i++)
                    {
                        int productID = workSheet.Cell(i, 1).GetValue<int>();
                        if (productID < 0)
                        {
                            returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                            returnData.ResponseMessenger = "Dữ liệu ID Import từ Excel không hợp lệ";
                            return returnData;
                        }

                        string productName = workSheet.Cell(i, 2).GetValue<string>();
                        if (!Validation.CheckSting(productName))
                        {
                            returnData.ReponseCode = (int)ProductInsertStatus.ProductNameNull;
                            returnData.ResponseMessenger = "ProductName Import từ Excel không hợp lệ";
                            return returnData;
                        }
                        if (!Validation.CheckXSSInput(productName))
                        {
                            returnData.ReponseCode = (int)ProductInsertStatus.InvalidXSSInput;
                            returnData.ResponseMessenger = "ProductName Import từ Excel không hợp lệ";
                            return returnData;
                        }

                        double productPrice = workSheet.Cell(i, 3).GetValue<double>();
                        if (productPrice < 0)
                        {
                            returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                            returnData.ResponseMessenger = "Dữ liệu ProductPrice Import từ Excel không hợp lệ";
                            return returnData;
                        }

                        int CategoryId = workSheet.Cell(i, 4).GetValue<int>();
                        if (CategoryId < 0)
                        {
                            returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                            returnData.ResponseMessenger = "Dữ liệu CategoryId Import từ Excel không hợp lệ";
                            return returnData;
                        }


                        var resultProduct = new Productt(productID, productName, productPrice, CategoryId);
                        _listProducts.Add(resultProduct);
                        returnData.ReponseCode = (int)ProductInsertStatus.Success;
                        returnData.ResponseMessenger = "Import thành công Product từ Excel";
                        return returnData;
                    }
                }
            }
            catch (Exception ex)
            {
                returnData.ReponseCode = -99;
                returnData.ResponseMessenger = ex.Message;
                return returnData;
            }
            return returnData;
        }
        public async Task<ProductData> ExportToExcel(string filePath)
        {
            var returnData = new ProductData();
            try
            {
                using (var workBook = new XLWorkbook())
                {
                    var workSheet = workBook.AddWorksheet("ProductFromBuiVanToan");
                    workSheet.Cell(1, 1).Value = "ProductID";
                    workSheet.Cell(1, 2).Value = "ProductName";
                    workSheet.Cell(1, 3).Value = "ProductPrice";
                    workSheet.Cell(1, 4).Value = "CategoryId";

                    int row = 2;
                    foreach (var product in _listProducts)
                    {
                        workSheet.Cell(row, 1).Value = product.ProductId;
                        workSheet.Cell(row, 2).Value = product.ProductName;
                        workSheet.Cell(row, 3).Value = product.ProductPrice;
                        workSheet.Cell(row, 4).Value = product.CategoryId;
                        row++;
                    }
                    workBook.SaveAs(filePath);
                    returnData.ReponseCode = (int)ProductInsertStatus.Success;
                    returnData.ResponseMessenger = "ExportToExcel thành cồng";
                    return returnData;
                }
            }
            catch (Exception ex)
            {
                returnData.ReponseCode = -99;
                returnData.ResponseMessenger = ex.Message;
                return returnData;
            }
        }
    
        public async Task<List<Productt>> SortProduct(int luaChon)
        {
            Console.WriteLine("Sắp Xếp Sản Phẩm ");
            Console.WriteLine("1. Sắp Xếp Tên Z-A");
            Console.WriteLine("2. Sắp Xếp Tên A-Z");
            Console.WriteLine("3. Sắp Xếp Gía Tăng");
            Console.WriteLine("4. Sắp Xếp Gía Giảm");

            List<Productt> sortedProducts;

            switch (luaChon)
            {
                case 1:
                    sortedProducts = _listProducts.OrderByDescending(p => p.ProductName).ToList();
                    break;

                case 2:
                    sortedProducts = _listProducts.OrderBy(p => p.ProductName).ToList();
                    break;

                case 3:
                    sortedProducts = _listProducts.OrderBy(p => p.ProductPrice).ToList();
                    break;

                case 4:
                    sortedProducts = _listProducts.OrderByDescending(p => p.ProductPrice).ToList();
                    break;

                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Trả về danh sách sản phẩm gốc.");
                    return await Task.FromResult(_listProducts);
            }
            return await Task.FromResult(sortedProducts);
        }
    }
}
