
using HotelReservationAPI.DTOs;
using HotelReservationAPI.Models;
using HotelReservationAPI.Repository;

namespace HotelReservationAPI.Service
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _roomRepository.GetAllAsync();
        }

        public async Task<Room?> GetRoomByIdAsync(int id)
        {
            return await _roomRepository.GetByIdAsync(id);
        }

        public async Task AddRoomAsync(Room room)
        {
            await _roomRepository.AddAsync(room);
        }

        public async Task UpdateRoomAsync(Room room)
        {
            await _roomRepository.UpdateAsync(room);
        }

        public async Task DeleteRoomAsync(int id)
        {
            await _roomRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(DateTime checkInDate, DateTime checkOutDate)
        {
            if (checkOutDate <= checkInDate)
            {
                throw new Exception("Çıkış tarihi, giriş tarihinden daha sonra olmalıdır!");
            }
            
            var rooms = await _roomRepository.GetAvailableRoomsAsync(checkInDate, checkOutDate);

            var roomDtos = rooms.Select(room => new RoomDto
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                Capacity = room.Capacity,
                PricePerNight = room.PricePerNight
            });

            return roomDtos;
        }

    }
}
