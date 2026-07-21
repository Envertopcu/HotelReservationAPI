

using HotelReservationAPI.Models;

namespace HotelReservationAPI.Repository
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllAsync(int? roomId = null, int? customerId = null, int pageNumber=1, int pageSize=10);
        Task<Reservation?> GetByIdAsync(int id);
        Task AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(int id);
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut);

    }
}
