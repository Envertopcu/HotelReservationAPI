

using HotelReservationAPI.Models;
using HotelReservationAPI.Repository;

namespace HotelReservationAPI.Service
{
    public class ReservationService : IReservationService
    {

        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _reservationRepository.GetAllAsync();
        }

        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            return await _reservationRepository.GetByIdAsync(id);
        }

        public async Task AddReservationAsync(Reservation reservation)
        {

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
