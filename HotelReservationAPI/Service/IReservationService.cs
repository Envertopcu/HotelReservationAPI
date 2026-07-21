

using HotelReservationAPI.Models;

namespace HotelReservationAPI.Service
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync(int? roomId = null, int? customerId = null, int pageNumber = 1, int pageSize = 10);
        Task<Reservation?> GetReservationByIdAsync(int id);
        Task AddReservationAsync(Reservation reservation);
        Task UpdateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(int id);

    }
}
