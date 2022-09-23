using _15.Models.Classes;
using _15.ViewModels;



Parking parking;
string serPath = "meow.txt";
string logPath = "meowLog.txt";

if (!File.Exists(serPath))
{
    parking = new Parking();
}
else
{
    FileStream file = new(serPath, FileMode.OpenOrCreate);
    parking = Parking.DeserializeBin(file);
    file.Close();
}

parking.CarValueChangedEvent += (prev, car) => 
{
    FileStream file = new (serPath, FileMode.OpenOrCreate);
    parking.SerializeBin(file);
    file.Close();
};

var menu = new MainMenu(parking, new FileStream(logPath, FileMode.OpenOrCreate));
menu.Start();



