using BE_102024.DataAces.NetCore.DAL;
using Microsoft.AspNetCore.Mvc;

namespace BE_10_2024_API.Controllers
{
    public class CategoryProduct : Controller
    {
        private ICategoryServis _categoryServis;
        public CategoryProduct(ICategoryServis categoryServis)
        {
            _categoryServis = categoryServis;
        }

        [HttpPost("InsertCategoryProduct")]
        public async Task<IActionResult> InsertCategoryProduct(BE_102024.DataAces.NetCore.DataOpject.CategoryProduct categoryProduct)
        {
            try
            {
                await _categoryServis.InsertCategory(categoryProduct);
                return Ok(new { Insert = "Insert thành công" });
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
