

using HotelReservationAPI.Data;
using HotelReservationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationAPI.Repository
{
    public class ReservationRepository : IReservationRepository
    {

        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync(int? roomId = null, int? customerId = null, int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Reservations
                        .Include(r => r.Customer)
                        .Include(r => r.Room)
                        .AsQueryable();
            if (roomId.HasValue)
            {
                query = query.Where(r => r.RoomId == roomId.Value);
            }

            if (customerId.HasValue)
            {
                query = query.Where(r => r.CustomerId == customerId.Value);
            }

            return await query.Skip((pageNumber - 1) * pageSize)
                      .Take(pageSize)
                      .ToListAsync();
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.Room)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            bool isConflict = await _context.Reservations.AnyAsync(r =>
                r.RoomId == roomId && 
                (
                    (checkIn >= r.CheckInDate && checkIn < r.CheckOutDate) ||
                    (checkOut > r.CheckInDate && checkOut <= r.CheckOutDate) ||
                    (checkIn <= r.CheckInDate && checkOut >= r.CheckOutDate)
                )
            );
            return !isConflict;
        }

    }
}
