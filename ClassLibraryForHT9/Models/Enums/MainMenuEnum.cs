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
                MainMenuEnum.PrintAllProducts => "Print_all_products".Replace('_', ' '),
                MainMenuEnum.CreateProduct => "Create_product".Replace('_',' '),
                MainMenuEnum.PrintProductsInInventory => "Print_products_in_inventory".Replace('_', ' '),
                MainMenuEnum.AddProductToInventory => "Add_product_to_inventory".Replace('_', ' '),
                MainMenuEnum.PrintInventoryCost => "Print_inventory_cost".Replace('_', ' '),
                MainMenuEnum.RemoveProductFromInventory => "Remove_product_from_inventory".Replace('_', ' '),
                MainMenuEnum.Quit => "Quit",
                _ => throw new ArgumentException("Value not supported",nameof(value))
            };
        }
    }
}
