

using HotelReservationAPI.Models;
using HotelReservationAPI.Repository;

namespace HotelReservationAPI.Service
{
    public class ReservationService : IReservationService
    {

        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;

        public ReservationService(IReservationRepository reservationRepository, IRoomRepository roomRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync(int? roomId = null, int? customerId = null, int pageNumber = 1, int pageSize = 10)
        {
            return await _reservationRepository.GetAllAsync(roomId,customerId, pageNumber, pageSize);
        }

        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            return await _reservationRepository.GetByIdAsync(id);
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            if (reservation.CheckOutDate <= reservation.CheckInDate)
            {
                throw new Exception("Çıkış tarihi, giriş tarihinden daha sonra olmalıdır!");
            }

            bool isAvailable = await _reservationRepository.IsRoomAvailableAsync(
                reservation.RoomId,
                reservation.CheckInDate,
                reservation.CheckOutDate);

            if (!isAvailable)
            {
                throw new Exception("Seçtiğiniz tarihlerde bu oda maalesef doludur.");
            }

            var room = await _roomRepository.GetByIdAsync(reservation.RoomId);
            if (room == null)
            {
                throw new Exception("Böyle bir oda sistemde bulunamadı!");
            }

            int nights = (reservation.CheckOutDate - reservation.CheckInDate).Days;

            if (nights == 0)
            {
                nights = 1;
            }

            reservation.TotalPrice = nights * room.PricePerNight;

            await _reservationRepository.AddAsync(reservation);
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
            await _reservationRepository.UpdateAsync(reservation);
        }

        public async Task DeleteReservationAsync(int id)
        {
            await _reservationRepository.DeleteAsync(id);
        }

    }
}
