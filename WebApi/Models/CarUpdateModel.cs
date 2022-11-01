using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class CarUpdateModel
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [MaxLength(10)]
        public string NewRegNumber { get; set; } = null!;
        [Range(1, int.MaxValue)]
        public int? NewClientId { get; set; }
        [Range(1, int.MaxValue)]
        public int NewCarType { get; set; }
    }
}
