

using HotelReservationAPI.DTOs;
using HotelReservationAPI.Models;

namespace HotelReservationAPI.Service
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync(int? roomId = null, int? customerId = null, int pageNumber = 1, int pageSize = 10);
        Task<ReservationDto?> GetReservationByIdAsync(int id);
        Task AddReservationAsync(Reservation reservation);
        Task UpdateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(int id);

    }
}
