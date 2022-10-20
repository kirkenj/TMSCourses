namespace WEB_EF.Models.Entities
{
    public partial class ParkingPlace
    {
        public ParkingPlace()
        {
            Journals = new HashSet<Journal>();
        }

        public int Id { get; set; }
        public int? CarType { get; set; }

        public virtual CarType? CarTypeNavigation { get; set; }
        public virtual ICollection<Journal> Journals { get; set; }
    }
}
