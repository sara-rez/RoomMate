using System.ComponentModel.DataAnnotations;

namespace RoomMate.Domain.BaseModels
{
    public abstract class BookingBase
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Date <= DateTime.Now.Date)
            {
                yield return new ValidationResult("Date cannot be in the past", new[] { nameof(Date) });
            }
        }
    }
}