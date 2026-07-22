namespace HotelReservationAPI.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }

        public string CustomerFullName { get; set; } = string.Empty;
        public string RoomNumber { get; set; } = string.Empty;

    }
}
