namespace WebApi.Models
{
    public class JournalItemModel
    {
        public int Id { get; set; }
        public DateTime ComingDate { get; set; }
        public int CarId { get; set; }
        public int ParkingPlace { get; set; }
        public DateTime? DepartureDate { get; set; }
    }
}