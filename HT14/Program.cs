using HT14.Models;

Employee executive = new Executive() { Name = "Manager", SalaryPerWeek = 300, TotalProjectsTurnover = 10^6, ManagersFee = 13, WorkWeeksAmm = 30};
Employee hourlyPaid = new HourlyEmployee() { Name = "Meow", SalaryPerHour = 12, WorkHoursAmm = 30};
executive.CopyFrom(hourlyPaid);
Console.WriteLine(executive);