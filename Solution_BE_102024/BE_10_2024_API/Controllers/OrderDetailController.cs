using BE102024.DataAces.NetCore.DAL_Interface;
using BE102024.DataAces.NetCore.DataOpject;
using BE102024.DataAces.NetCore.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_102024.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        IOrderDetailRepository _orderDetailRepository;
        IUnitOfWork _unitOfWork;
        public OrderDetailController(IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork)
        {
            //_orderDetailRepository = orderDetailRepository;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("CreateOrderDetail")]
        public async Task<IActionResult> CreateOrderDetail([FromBody] CreateOrderRequest requestInsert)
        {
            try
            {
                var resultOrder = await _unitOfWork._orderRepository.InsertOrder(requestInsert.order);

                var resultpRroduct = await _unitOfWork._productRepository.InsertProduct(requestInsert.product);

                var resultOrderDetail = await _unitOfWork._orderDetailRepository.InsertOrderDetail(requestInsert.orderDetail);


                var resultRequest = await _unitOfWork.SaveChange();
                return Ok(resultRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpPost("UpdateOrderDetail")]
        //public async Task<IActionResult> UpdateOrderDetail([FromBody] UpdateOrderDetail updateOrderDetail)
        //{
        //    try
        //    {
        //        var rs = await _orderDetailRepository.UpdateOrderDetail(updateOrderDetail);
        //        return Ok(rs);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpGet("GetAllOrderDetail")]
        //public async Task<IActionResult> GetAllOrderDetail([FromBody] string? ProductName, int? OrderDetailId)
        //{
        //    try
        //    {
        //        var rs = await _orderDetailRepository.GetAllOrderDetails(ProductName, OrderDetailId);
        //        return Ok(rs);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpDelete("DeleteOrderDetail")]
        //public async Task<IActionResult> GetAllOrderDetail([FromBody] string? ProductName, int? OrderDetailId)
        //{
        //    try
        //    {
        //        var rs = await _orderDetailRepository.GetAllOrderDetails(ProductName, OrderDetailId);
        //        return Ok(rs);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
