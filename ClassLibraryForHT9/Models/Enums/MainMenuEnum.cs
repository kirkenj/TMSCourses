namespace ClassLibraryForHT9.Models.Enums
{
    public enum MainMenuEnum
    {
        CreateProduct,
        PrintProductsInInventory,
        PrintInventoryCost,
        FindProductByID,
        FindProductByTitle,
        ReplaceFoundProductByIDWithNew,
        ReplaceFoundProductByTitleWithNew,
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
                MainMenuEnum.FindProductByID => "Find product by ID",
                MainMenuEnum.ReplaceFoundProductByIDWithNew => "Replace found product by ID with default new",
                MainMenuEnum.ReplaceFoundProductByTitleWithNew => "Replace found product by Title with default new",
                MainMenuEnum.FindProductByTitle => "Find product by Title",
                MainMenuEnum.Quit => "Quit",
                _ => throw new ArgumentException("Value not supported",nameof(value))
            };
        }
    }
}
