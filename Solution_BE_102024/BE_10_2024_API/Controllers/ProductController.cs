using BE_102024.DataAces.NetCore.CheckConditions;
using BE_102024.DataAces.NetCore.DAL;
using BE_102024.DataAces.NetCore.DataOpject;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BE_10_2024_API.Controllers
{
    public class ProductController : Controller
    {
        //private IProductRepositor _productRepositor;

        //public ProductController(IProductRepositor productRepositor)
        //{
        //    _productRepositor = productRepositor;
        //}
        //[HttpPost("InsertProduct")]
        //public async Task<IActionResult> InsertProduct(Productt product)
        //{
        //    try
        //    {
        //        var result = await _productRepositor.InsertProduct(product);
        //        if (result.ReponseCode == (int)ProductInsertStatus.Success)
        //        {
        //            return Ok(new { Insert = "Insert thành công" });
        //        }
        //        else return BadRequest(new { Error = result.ResponseMessenger });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        //[HttpPost("GetAllProduct")]
        //public async Task<IActionResult> GetAllListProduct()
        //{
        //    try
        //    {
        //        var list = await _productRepositor.GetAllListProduct();
        //        if (list == null)
        //        {
        //            return NotFound("Danh sách Product rỗng");
        //        }
        //        else return Ok(new { product = list });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpDelete("DeleteProductByID")]
        //public async Task<IActionResult> DeleteProductByID(int productID)
        //{
        //    try
        //    {
        //        await _productRepositor.DeleteProductById(productID);
        //        return Ok(new { Delete = "Delete thành công" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpGet("GetProductByProductOfCategory")]
        //public async Task<IActionResult> GetProductByProductOfCategory(string? productName, string? categoryName)
        //{
        //    try
        //    {
        //        var result = await _productRepositor.GetProductByProductOfCategory(productName, categoryName);
        //        if (result != null && result.Count > 0)
        //        {
        //            return Ok(new { Product = result });
        //        }
        //        else
        //        {
        //            return NotFound($"Không tìm thấy sản phẩm có tên: {productName} " +
        //                $"|| Loại sản phẩm có tên: {categoryName}.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("ExportToExcel")]
        //public async Task<IActionResult> ExportToExcel(string filePath)
        //{
        //    try
        //    {
        //        var result = await _productRepositor.ExportToExcel(filePath);
        //        if (result.ReponseCode == (int)ProductInsertStatus.Success)
        //        {
        //            return Ok(new { mesage = "ExportToExcel thành cồng" });
        //        }
        //        else return BadRequest(new { Error = result.ResponseMessenger });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpGet("SortProduct")] 
        //public async Task<IActionResult> SortProduct(int luaChon)
        //{
        //    try
        //    {
        //        var result = await _productRepositor.SortProduct(luaChon);
        //        return Ok(result);

        //    }
        //    catch(Exception ex) 
        //    {
        //        return BadRequest(new {Error = ex.Message});
        //    }
        //}
    }
}
