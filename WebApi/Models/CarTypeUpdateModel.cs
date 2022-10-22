namespace WebApi.Models
{
    public class CarTypeUpdateModel
    {
        public int Id { get; set; }
        public string NewTypeName { get; set; } = null!;
    }
}
