using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelReservationAPI.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Oda numarası zorunludur.")]
        [StringLength(10)]
        public required string RoomNumber { get; set; }

        [Required(ErrorMessage = "Kapasite bilgisi zorunludur.")]
        [Range(1, 10, ErrorMessage = "Kapasite 1 ile 10 arasında olmalıdır.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Gecelik fiyat zorunludur.")]
        [Range(0, double.MaxValue, ErrorMessage = "Geçerli bir fiyat giriniz.")]
        public decimal PricePerNight { get; set; }
        
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
