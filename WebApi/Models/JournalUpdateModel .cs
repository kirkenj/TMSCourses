using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class JournalUpdateModel : IValidatableObject
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        public DateTime NewComingDate { get; set; }
        [Range(1, int.MaxValue)]
        public int NewCarId { get; set; }
        [Range(1, int.MaxValue)]
        public int NewParkingPlace { get; set; }
        public DateTime? NewDepartureDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NewDepartureDate != null && NewDepartureDate < NewComingDate)
            {
                yield return new ValidationResult("Departure date can not be less coming date");
            }
        }
    }
}