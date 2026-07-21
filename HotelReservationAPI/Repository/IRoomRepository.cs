

using HotelReservationAPI.Models;

namespace HotelReservationAPI.Repository
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(int id);
        Task AddAsync(Room room);
        Task UpdateAsync(Room room);
        Task DeleteAsync(int id);
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkInDate, DateTime checkOutDate);

    }
}
