namespace WebApi.Models
{
    public class JournalCreateModel
    {
        public DateTime ComingDate { get; set; }
        public int CarId { get; set; }
        public int ParkingPlace { get; set; }
        public DateTime? DepartureDate { get; set; }
    }
}