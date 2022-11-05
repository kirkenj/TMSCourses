namespace WebApiDatabase.Entities
{
    public partial class Car
    {
        public Car()
        {
            Journals = new HashSet<Journal>();
        }

        public string RegNumber { get; set; } = null!;
        public int? ClientId { get; set; }
        public int CarType { get; set; }
        public int Id { get; set; }

        public virtual CarType CarTypeNavigation { get; set; } = null!;
        public virtual Client? Client { get; set; } = null!;
        public virtual ICollection<Journal> Journals { get; set; }
    }
}
