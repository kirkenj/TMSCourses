using HT14.Models;
using static testRepo.Programm;

var company = new Company();
var menuPositions = Enum.GetValues(typeof(MainMenuPositions)).Cast<MainMenuPositions>().ToArray();
Dictionary<MainMenuPositions, Action> menu = new()
{
    {MainMenuPositions.SetPost, company.ShowSetPostMenu },
    {MainMenuPositions.PrintEmployees, company.PrintEmployees },
    {MainMenuPositions.FireEmployee, company.ShowFireMenu },
    {MainMenuPositions.AddEmployee, company.ShowAddEmployeeMenu },
    {MainMenuPositions.EditEmployee, company.ShowEmployeeEditMenu }
};

while (true)
{
    var selection = SeletctItemFromArray("MAIN MENU\nselect option", menuPositions);
    if (selection == default)
    {
        Console.WriteLine("Bye");
        break;
    }

    try
    {
        menu[selection]();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}