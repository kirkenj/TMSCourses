using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class JournalCreateModel : IValidatableObject
    {
        
        public DateTime ComingDate { get; set; }
        [Range(1,int.MaxValue)]
        public int CarId { get; set; }
        [Range(1, int.MaxValue)]
        public int ParkingPlace { get; set; }
        public DateTime? DepartureDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DepartureDate != null && DepartureDate < ComingDate)
            {
                yield return new ValidationResult("Departure date can not be less coming date");
            }
        }
    }
}