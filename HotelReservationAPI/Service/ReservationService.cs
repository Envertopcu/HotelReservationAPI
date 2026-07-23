

using HotelReservationAPI.DTOs;
using HotelReservationAPI.Models;
using HotelReservationAPI.Repository;

namespace HotelReservationAPI.Service
{
    public class ReservationService : IReservationService
    {

        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ICustomerRepository _customerRepository;

        public ReservationService(IReservationRepository reservationRepository, IRoomRepository roomRepository, ICustomerRepository customerRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync(int? roomId = null, int? customerId = null, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentException("Sayfa numarası en az 1 olmalıdır.");
            }

            pageSize = Math.Clamp(pageSize, 1, 100);
            var reservations = await _reservationRepository.GetReservationsAsync(roomId, customerId, pageNumber, pageSize);
            var reservationDtos = reservations.Select(res => new ReservationDto
            {

                Id = res.Id,
                CheckInDate = res.CheckInDate,
                CheckOutDate = res.CheckOutDate,
                TotalPrice = res.TotalPrice,

                CustomerFullName = res.Customer != null ? $"{res.Customer.FirstName} {res.Customer.LastName}" : "Bilinmiyor",

                RoomNumber = res.Room != null ? res.Room.RoomNumber : "Bilinmiyor"
            });
            return reservationDtos;

        }

        public async Task<ReservationDto?> GetReservationByIdAsync(int id)
        {
            var res = await _reservationRepository.GetByIdAsync(id);

            if (res == null)
            {
                return null;
            }

            return new ReservationDto
            {
                Id = res.Id,
                CheckInDate = res.CheckInDate,
                CheckOutDate = res.CheckOutDate,
                TotalPrice = res.TotalPrice,
                CustomerFullName = res.Customer != null ? $"{res.Customer.FirstName} {res.Customer.LastName}" : "Bilinmiyor",
                RoomNumber = res.Room != null ? res.Room.RoomNumber : "Bilinmiyor"
            };
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            reservation.TotalPrice = await CalculateTotalPriceAsync(
            reservation.CustomerId,
            reservation.RoomId,
            reservation.CheckInDate,
            reservation.CheckOutDate);

            await _reservationRepository.AddAsync(reservation);
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
            var existingReservation =
        await _reservationRepository.GetByIdAsync(reservation.Id);

            if (existingReservation is null)
            {
                throw new KeyNotFoundException("Rezervasyon bulunamadı.");
            }

            var totalPrice = await CalculateTotalPriceAsync(
                reservation.CustomerId,
                reservation.RoomId,
                reservation.CheckInDate,
                reservation.CheckOutDate,
                reservation.Id);

            existingReservation.CustomerId = reservation.CustomerId;
            existingReservation.RoomId = reservation.RoomId;
            existingReservation.CheckInDate = reservation.CheckInDate;
            existingReservation.CheckOutDate = reservation.CheckOutDate;
            existingReservation.TotalPrice = totalPrice;

            await _reservationRepository.UpdateAsync(existingReservation);
        }

        public async Task DeleteReservationAsync(int id)
        {
            await _reservationRepository.DeleteAsync(id);
        }
        private async Task<decimal> CalculateTotalPriceAsync(
            int customerId,
            int roomId,
            DateTime checkInDate,
            DateTime checkOutDate,
            int? excludedReservationId = null)
        {
            if (checkOutDate <= checkInDate)
            {
                throw new ArgumentException(
                    "Çıkış tarihi giriş tarihinden sonra olmalıdır.");
            }

            var customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer is null)
            {
                throw new ArgumentException("Müşteri bulunamadı.");
            }

            var room = await _roomRepository.GetByIdAsync(roomId);

            if (room is null)
            {
                throw new ArgumentException("Oda bulunamadı.");
            }

            var isAvailable = await _reservationRepository.IsRoomAvailableAsync(
                roomId,
                checkInDate,
                checkOutDate,
                excludedReservationId);

            if (!isAvailable)
            {
                throw new ArgumentException(
                    "Bu oda seçilen tarihlerde dolu.");
            }

            var nights = Math.Max(
                1,
                (int)Math.Ceiling((checkOutDate - checkInDate).TotalDays));

            return nights * room.PricePerNight;
        }
    }
}
