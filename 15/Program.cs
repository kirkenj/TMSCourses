using _15.Models.Classes;
using _15.Models.Enums;
using _15.ViewModels;
using System.Runtime.Serialization.Formatters.Binary;



BinaryFormatter binaryFormatter = new();
var parking = new Parking();

parking.NotifyCarValueChanged += PrinkCarValueChanged;
var created = new Car(Fuel.Petrol, 2000, 1000, "Meow");
parking.AddCar(created);
FileStream file = new("meow.txt", FileMode.OpenOrCreate);
parking.NotifyCarValueChanged -=PrinkCarValueChanged;
binaryFormatter.Serialize(file, parking);

void PrinkCarValueChanged(Car oldVal, Car newVal) 
{
    Console.WriteLine(oldVal.Equals(default) ? String.Empty : $"Car added {newVal}");
};

//file.Close();

//_streamWriter = new("logs.txt", true);
//ParkingChanged += SaveBinary;
//Notify += Console.WriteLine;
//Notify += loggs.Push;
//Notify += _streamWriter.WriteLine;
//dict.Add(ParkingMenu.AddCar, AddCar);
//dict.Add(ParkingMenu.EditCar, EditCar);
//dict.Add(ParkingMenu.RemoveCar, RemoveCar);
//dict.Add(ParkingMenu.SendCarForARide, SendCarForARide);
//dict.Add
//(
//    ParkingMenu.SortCarsByPower, () =>
//    {
//        Console.WriteLine(String.Join("\n", _cars.OrderBy(c => c.EnginePower)));
//    }
//);
//dict.Add(ParkingMenu.SortCarsByDefault, () =>
//{
//    if (!_cars.Any())
//    {
//        return;
//    }

//    var arr = _cars.ToArray();
//    Array.Sort(arr);
//    Console.WriteLine(String.Join("\n", arr.ToList()));
//});
//dict.Add(ParkingMenu.PrintCars, PrintCars);
//dict.Add(ParkingMenu.FillCarTank, FillCarTank);
//dict.Add(ParkingMenu.PrintLogs, () => Console.WriteLine(string.Join("\n", loggs)));

//var parking = new Parking();
//parking.Start();
//parking.Dispose();


