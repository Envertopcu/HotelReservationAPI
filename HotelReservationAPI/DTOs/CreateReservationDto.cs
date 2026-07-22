namespace HotelReservationAPI.DTOs
{
    public class CreateReservationDto
    {
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

    }
}
