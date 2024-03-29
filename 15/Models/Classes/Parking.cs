﻿using _15.Models.Enums;
using _15.Models.Structs;
using System.Text.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
#pragma warning disable SYSLIB0011 // Тип или член устарел

namespace _15.Models.Classes
{
    [Serializable]
    public class Parking
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

        [field: NonSerialized]
        public event CarValueChanged? CarValueChangedEvent;

        public Parking()
        {
            
        }

        public void AddCar(Car car)
        {
            if (car != null)
            {
                _cars.Add(car);
                CarValueChangedEvent?.Invoke(null, car);
            }
        }

        #region EditCar
        public void EditCar(Car car, CarStruct? values)
        {
            if (car == null)
            {
                return;
            }

            var prevVal = car.GetStruct();
            car.Edit(values);
            CarValueChangedEvent?.Invoke(prevVal, car);
        }

        public void EditCar(Car car, Fuel fuel, int enginePower, int tankCapacity, string identifier)
        {
            if (car == null)
            {
                return;
            }

            var prev = car.GetStruct();
            car.Edit(fuel, enginePower, tankCapacity, identifier);
            CarValueChangedEvent?.Invoke(prev, car);
        }
        #endregion

        public void RemoveCar(Car car)
        {
            if (car != null)
            {
                _cars.Remove(car); 
                CarValueChangedEvent?.Invoke(car.GetStruct(), null);
            }
        }

        public void SendCarForARide(Car car)
        {
            if (car == null || !_cars.Contains(car))
            {
                return;
            }

            var prevValue = car.GetStruct();
            car.Ride(); 
            CarValueChangedEvent?.Invoke(prevValue, car);
        }

        public int FillCarTank(Car car, int volume)
        {
            if (car == null)
            {
                return volume;
            }

            var prevValue = car.GetStruct();
            var extraFuel = car.FillTank(car.Fuel, volume);
            var newValue = car;
            CarValueChangedEvent?.Invoke(prevValue, car);
            return extraFuel;
        }

        public override string ToString() => string.Join("\n", _cars);
        public delegate void CarValueChanged(CarStruct? prevValue, Car? newValue);

        public void SerializeBin(FileStream fileStream)
        {
            BinaryFormatter binaryFormatter = new();
            binaryFormatter.Serialize(fileStream, this);
        }

        public void SerializeJson(FileStream fileStream) => JsonSerializer.Serialize(fileStream, this);
        public void SerializeXML(FileStream fileStream)
        {
            XmlSerializer serializer = new (typeof(Parking));
            serializer.Serialize(fileStream, this);
        }

        public static Parking DeserializeBin(FileStream fileStream)
        {
            BinaryFormatter binaryFormatter = new();
            return (Parking)binaryFormatter.Deserialize(fileStream);
        }
    }
}
