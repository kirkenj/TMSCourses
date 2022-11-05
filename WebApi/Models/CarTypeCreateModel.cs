using System.ComponentModel.DataAnnotations;
using WebApi.Models.Attributes;

namespace WebApi.Models
{
    public class CarTypeCreateModel : IValidatableObject
    {
        [MaxLength(10)]
        [NameValidation(1, ' ', ErrorMessage = "Name consists of 2 or more words")]
        public string TypeName { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(TypeName))
            {
                yield return new ValidationResult("Registration number can not be null or empty");
            }

            if (TypeName.Length > 10)
            {
                yield return new ValidationResult("Car type name's length can not be more than 10");
            }
        }
    }
}
