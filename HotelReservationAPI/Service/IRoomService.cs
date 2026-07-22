
using HotelReservationAPI.Models;
using HotelReservationAPI.DTOs;

namespace HotelReservationAPI.Service
{
    public interface IRoomService
    {

        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room?> GetRoomByIdAsync(int id);
        Task AddRoomAsync(Room room);
        Task UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(int id);
        Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(DateTime checkInDate, DateTime checkOutDate);

    }
}
