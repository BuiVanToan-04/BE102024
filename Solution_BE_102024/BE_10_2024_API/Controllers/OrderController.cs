using BE102024.DataAces.NetCore.DAL_Interface;
using BE102024.DataAces.NetCore.DataOpject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_102024.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        
        
        [HttpPost("CreateOrder")]        
        public async Task<IActionResult> CreateOrder(InsertOrder insertOrder)
        {
            try
            {
                var rs = await _orderRepository.InsertOrder(insertOrder);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
