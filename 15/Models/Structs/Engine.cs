using _15.Models.Enums;
using _15.Models.Interfaces;
using static testRepo.Programm;

namespace _15.Models.Classes
{
    [Serializable]
    internal struct Engine : IEngine
    {
        [NonSerialized]
        private static readonly Fuel[] _fuelTypes = Enum.GetValues(typeof(Fuel)).Cast<Fuel>().ToArray();

        public Fuel Fuel { get; set; } = 0;

        public int Power { get; set; } = 0;

        public Engine(Fuel fuel, int power)
        {
            if (power < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(power));
            }

            Power = power;
            Fuel = fuel;
        }

        public Engine()
        {

        }

        public override string ToString() => $"Power: {Power}, Fuel: {Fuel}";

        public static Engine? CreateEngineByUser()
        {
            var fuel = SeletctItemFromArray("Select engine's fuel", _fuelTypes);
            if (fuel == default)
            {
                return null;
            }

            var power = ReadIntFromConsole("Input engine's power", 0, int.MaxValue);
            return new Engine(fuel, power);
        }
    }
}
