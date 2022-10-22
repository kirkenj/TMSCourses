namespace WebApi.Models
{
    public class CarCreateModel
    {
        public string RegNumber { get; set; } = null!;
        public int? ClientId { get; set; }
        public int CarType { get; set; }
    }
}
