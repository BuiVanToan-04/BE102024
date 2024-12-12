using BE_102024.DataAces.NetCore.DAL;
using BE_102024.DataAces.NetCore.DataOpject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BE_10_2024_API.Filter;

namespace BE_10_2024_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private IRoomRepository _roomRepository;
        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [BE_102024_Authorization("GetAllRoom", "VIEW")]
        [HttpGet("GetAllRoom")]
        public async Task<IActionResult> GetAllRoom(string? roomName)
        {
            try
            {
                var listRoom = await _roomRepository.GetAllRoom(roomName);
                //var listRoom = await _genericRoomRepository.GetAll();
                return Ok(listRoom);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertRoom")]
        public async Task<IActionResult> InsertRoom(Room_InsertRequestData room)
        {
            try
            {
                var result = await _roomRepository.InsertRoom(room);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateRoom")]
        public async Task<IActionResult> UpdateRoom(Room_InsertRequestData room)
        {
            try
            {
                var result = await _roomRepository.UpdateRoom(room); 
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("DeleteRoom")]
        public async Task<IActionResult> DeleteRoom(int roomID)
        {
            try
            {
                var result = await _roomRepository.DeleteRoom(roomID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
