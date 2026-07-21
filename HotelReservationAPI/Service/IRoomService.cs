
using HotelReservationAPI.Models;

namespace HotelReservationAPI.Service
{
    public interface IRoomService
    {

        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room?> GetRoomByIdAsync(int id);
        Task AddRoomAsync(Room room);
        Task UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(int id);
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkInDate, DateTime checkOutDate);

    }
}
