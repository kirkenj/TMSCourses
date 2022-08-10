using _15.Models.Enums;


namespace _15.Models.Interfaces
{
    public interface IEngine
    {
        public Fuel Fuel { get; }
        public int Power { get; }
    }
}
