using _15.Models.Classes;
using _15.Models.Enums;
using System.Runtime.Serialization.Formatters.Binary;
using static testRepo.Programm;

namespace _15.ViewModels
{
    [Serializable]
    internal class Parking
    {
        private List<Car> _cars = new();
        public Car[] Cars
        {
            get => _cars.ToArray();
            set 
            {
                _cars = value.ToList();
            }
        }
        public event CarValueChanged? NotifyCarValueChanged = null;

        public Parking()
        {
            
        }

        public void AddCar(Car car)
        {
            if (!car.Equals(default))
            {
                _cars.Add(car);
                NotifyCarValueChanged?.Invoke(default, car);
            }
        }

        public void RemoveCar(Car car)
        {
            if (!car.Equals(default))
            {
                _cars.Remove(car); 
                NotifyCarValueChanged?.Invoke(car, default);
            }
        }

        public void SendCarForARide(Car car)
        {
            if (car.Equals(default) || !_cars.Contains(car))
            {
                return;
            }

            var prevValue = car;
            car.Ride(); 
            NotifyCarValueChanged?.Invoke(prevValue, car);
        }

        public int FillCarTank(Car car, int volume)
        {
            if (car.Equals(default))
            {
                return volume;
            }

            var prevValue = car;
            var extraFuel = car.FillTank(car.Fuel, volume);
            var newValue = car;
            NotifyCarValueChanged?.Invoke(prevValue, car);
            return extraFuel;
        }

        public override string ToString() => string.Join("\n", _cars);
        public delegate void CarValueChanged(Car prevValue, Car newValue);
    }
}
