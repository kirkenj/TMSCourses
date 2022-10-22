namespace WebApi.Models
{
    public class CarUpdateModel
    {
        public int Id { get; set; }
        public string NewRegNumber { get; set; } = null!;
        public int? NewClientId { get; set; }
        public int NewCarType { get; set; }
    }
}
