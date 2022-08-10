using _15.Models.Enums;
using _15.Models.Interfaces;

namespace _15.Models.Classes
{
    internal class Engine : IEngine
    {
        public Fuel Fuel { get; private set; }

        public int Power { get; private set; }

        public void ChangeFuel(Fuel newFuel)
        {
            Fuel = newFuel;
        }

        public void ChangePower(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            Power = value;
        }

        public Engine(Fuel fuel, int power)
        {
            if (power < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(power));
            }

            Power = power;
            Fuel = fuel;
        }
    }
}
