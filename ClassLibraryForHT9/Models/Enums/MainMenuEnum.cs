namespace ClassLibraryForHT9.Models.Enums
{
    public enum MainMenuEnum
    {
        PrintAllProducts = 1,
        CreateProduct = 2,
        PrintProductsInInventory = 3,
        AddProductToInventory = 4,
        PrintInventoryCost = 5,
        RemoveProductFromInventory = 6,
        Quit
    }

    public static class MainMenuEnumToStringConvertor
    {
        public static string ToString(MainMenuEnum value)
        {
            return value switch
            {
                MainMenuEnum.PrintAllProducts => "Print all products",
                MainMenuEnum.CreateProduct => "Create product",
                MainMenuEnum.PrintProductsInInventory => "Print products in inventory",
                MainMenuEnum.AddProductToInventory => "Add product to inventory",
                MainMenuEnum.PrintInventoryCost => "Print inventory cost",
                MainMenuEnum.RemoveProductFromInventory => "Remove product from inventory",
                MainMenuEnum.Quit => "Quit",
                _ => throw new ArgumentException("Value not supported",nameof(value))
            };
        }
    }
}
