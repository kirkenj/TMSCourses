using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class ParkingPlaceUpdateModel
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Range(1, int.MaxValue)]
        public int NewCarType { get; set; }
    }
}
