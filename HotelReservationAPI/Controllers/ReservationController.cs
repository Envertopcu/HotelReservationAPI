

using HotelReservationAPI.Models;
using HotelReservationAPI.Service;
using Microsoft.AspNetCore.Mvc;
using HotelReservationAPI.DTOs;

namespace HotelReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {

        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations(
            [FromQuery] int? roomId,
            [FromQuery] int? customerId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                var reservations = await _reservationService.GetAllReservationsAsync(roomId, customerId, pageNumber, pageSize);

                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Rezervasyonlar getirilirken sunucu tarafında bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetReservation(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null) return NotFound();
            return Ok(reservation);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReservation(CreateReservationDto createDto)
        {
            try
            {
                var newReservation = new Reservation
                {
                    CustomerId = createDto.CustomerId,
                    RoomId = createDto.RoomId,
                    CheckInDate = createDto.CheckInDate,
                    CheckOutDate = createDto.CheckOutDate
                };

                await _reservationService.AddReservationAsync(newReservation);

                return Ok("Rezervasyon başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id) return BadRequest();
            await _reservationService.UpdateReservationAsync(reservation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return NoContent();
        }

    }
}
