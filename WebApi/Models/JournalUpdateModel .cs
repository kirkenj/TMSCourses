namespace WebApi.Models
{
    public class JournalUpdateModel
    {
        public int Id { get; set; }
        public DateTime NewComingDate { get; set; }
        public int NewCarId { get; set; }
        public int NewParkingPlace { get; set; }
        public DateTime? NewDepartureDate { get; set; }
    }
}