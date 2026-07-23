

using System.ComponentModel.DataAnnotations;

namespace HotelReservationAPI.DTOs
{
    public class UpdateReservationDto : IValidatableObject
    {
        [Range(1, int.MaxValue)]
        public int CustomerId { get; set; }

        [Range(1, int.MaxValue)]
        public int RoomId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CheckInDate == default)
            {
                yield return new ValidationResult(
                    "Giriş tarihi zorunludur.",
                    new[] { nameof(CheckInDate) });
            }

            if (CheckOutDate == default)
            {
                yield return new ValidationResult(
                    "Çıkış tarihi zorunludur.",
                    new[] { nameof(CheckOutDate) });
            }
        }
    }
}
