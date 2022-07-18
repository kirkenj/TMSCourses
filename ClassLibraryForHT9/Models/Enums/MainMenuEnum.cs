namespace ClassLibraryForHT9.Models.Enums
{
    public enum MainMenuEnum
    {
        CreateProduct,
        PrintProductsInInventory,
        PrintInventoryCost,
        FindProductByID,
        FindProductByTitle,
        RemoveProductFromInventory,
        Quit
    }

    public static class MainMenuEnumToStringConvertor
    {
        public static string ToString(MainMenuEnum value)
        {
            return value switch
            {
                MainMenuEnum.CreateProduct => "Create product",
                MainMenuEnum.PrintProductsInInventory => "Print products in inventory",
                MainMenuEnum.PrintInventoryCost => "Print inventory cost",
                MainMenuEnum.RemoveProductFromInventory => "Remove product from inventory",
                MainMenuEnum.FindProductByID => "Find product",
                MainMenuEnum.Quit => "Quit",
                _ => throw new ArgumentException("Value not supported",nameof(value))
            };
        }
    }
}
