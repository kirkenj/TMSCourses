using _15.Models.Enums;

namespace _15.Models.Interfaces
{
    public interface ICar<T> where T : IEngine
    {
        public T? Engine { get; }
        public int FuelTankCapacity { get; }
        public string Identifier { get; }
        public int FuelLevel { get; }
        public int FillTank(Fuel fuel, int volume);
        public void Ride();
    }
}
