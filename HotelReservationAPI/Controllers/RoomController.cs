
using HotelReservationAPI.Models;
using HotelReservationAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {

        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<Room>>> GetAvailableRooms(
        [FromQuery] DateTime checkInDate,
        [FromQuery] DateTime checkOutDate)
        {
            try
            {
                var availableRooms = await _roomService.GetAvailableRoomsAsync(checkInDate, checkOutDate);
                return Ok(availableRooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateRoom(Room room)
        {
            await _roomService.AddRoomAsync(room);
            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRoom(int id, Room room)
        {
            if (id != room.Id) return BadRequest();
            await _roomService.UpdateRoomAsync(room);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRoom(int id)
        {
            await _roomService.DeleteRoomAsync(id);
            return NoContent();
        }

    }
}
