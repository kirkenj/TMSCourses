namespace WebApi.Models
{
    public class ClientUpdateModel
    {
        public int Id { get; set; }
        public string NewName { get; set; } = null!;
        public string NewSurname { get; set; } = null!;
    }
}