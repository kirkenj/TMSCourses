using _15.Models.Enums;
using _15.Models.Interfaces;
using _15.Models.Structs;
using static testRepo.Programm;

namespace _15.Models.Classes
{
    [Serializable]
    public class Engine : IEngine
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

        public EngineStruct GetStruct() => new EngineStruct() { Fuel = this.Fuel, Power = this.Power };
        public override string ToString() => $"Power: {Power}, Fuel: {Fuel}";
    }
}
