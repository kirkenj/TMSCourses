namespace WEB_EF.Models.Entities
{
    public partial class Journal
    {
        public int Id { get; set; }
        public DateTime ComingDate { get; set; }
        public int CarId { get; set; }
        public int ParkingPlace { get; set; }
        public DateTime? DepartureDate { get; set; }

        public virtual Car Car { get; set; } = null!;
        public virtual ParkingPlace ParkingPlaceNavigation { get; set; } = null!;
    }
}
