using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class CarCreateModel : IValidatableObject
    {
        public string RegNumber { get; set; } = null!;
        public int? ClientId { get; set; }
        public int CarType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(RegNumber))
            {
                errors.Add(new ValidationResult("Registration number can not be null or empty"));
            }

            if (RegNumber.Length > 10)
            {
                errors.Add(new ValidationResult("Registration number's length can not be more than 10"));
            }

            if (ClientId != null && ClientId < 0)
            {
                errors.Add(new ValidationResult("Owner's ID can not be less 0"));
            }

            if (ClientId != null && ClientId < 0)
            {
                errors.Add(new ValidationResult("Car type's ID can not be less 0"));
            }

            return errors;
        }
    }
}
