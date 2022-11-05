using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class CarTypeUpdateModel
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [MaxLength(10)]
        public string NewTypeName { get; set; } = null!;
    }
}
