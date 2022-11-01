using System.ComponentModel.DataAnnotations;
using WebApi.Models.Attributes;

namespace WebApi.Models
{
    public class ClientUpdateModel
    {
        [Range(1,int.MaxValue)]
        public int Id { get; set; }
        [MaxLength(10)]
        [NameValidation(1, ' ', ErrorMessage = "Name consists of 2 or more words")]
        [RegularExpression("[a-zA-Z]")]
        public string NewName { get; set; } = null!;
        [MaxLength(10)]
        [NameValidation(1, ' ', ErrorMessage = "Name consists of 2 or more words")]
        [RegularExpression("[a-zA-Z]")]
        public string NewSurname { get; set; } = null!;
    }
}