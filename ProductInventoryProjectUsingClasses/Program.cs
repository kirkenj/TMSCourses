using static testRepo.Programm;
using ClassLibraryForHT9.Models;
using ClassLibraryForHT9.Models.Enums;
#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.

List<Product> allProducts = new()
{
    new Product(),
    new Product("product2"),
    new Product(12),
    new Product(null, 12),
};

ProductInventory inventory = new(allProducts.ToArray());
MainMenuEnum[] mainMenuEnumArr = Enum.GetValues(typeof(MainMenuEnum)).Cast<MainMenuEnum>().ToArray();
ProductCreationMenuEnum[] productCreationMenuEnumArr = Enum.GetValues(typeof(ProductCreationMenuEnum)).Cast<ProductCreationMenuEnum>().ToArray();
MainMenuEnum option;
ProductCreationMenuEnum productCreationOption;
int productSelectionIndex;
var m = inventory[null];
Console.WriteLine(m);


while (true)
{
    try
    {
        option = mainMenuEnumArr[SelectItemIndexFromArray("PRODUCT INVENTORY PROJECT - CLASSES", mainMenuEnumArr.Select(e => MainMenuEnumToStringConvertor.ToString(e)).ToArray(), false)];
        switch (option)
        {
            case MainMenuEnum.CreateProduct:
                productCreationOption = productCreationMenuEnumArr[SelectItemIndexFromArray("PRODUCT CREATING MENU", productCreationMenuEnumArr.Select(e => ProductCreationMenuEnumConvertor.ToString(e)).ToArray(), false)];
                switch (productCreationOption)
                {
                    case ProductCreationMenuEnum.CreateByTitle:
                        Console.WriteLine("Input product title:");
                        inventory.Add(new Product(Console.ReadLine()));
                        break;
                    case ProductCreationMenuEnum.CreateByPrice:
                        inventory.Add(new Product(ReadIntFromConsole("Input product cost")));
                        break;
                    case ProductCreationMenuEnum.CreateByDefault:
                        inventory.Add(new Product());
                        break;
                    case ProductCreationMenuEnum.CreateByPriceAndTitle:
                        Console.WriteLine("Input product title:");
                        inventory.Add(new Product(Console.ReadLine(), ReadIntFromConsole("Input product cost")));
                        break;
                }
                
                Console.WriteLine($"Product added - {allProducts.Last()}");
                break;

            case MainMenuEnum.PrintProductsInInventory:
                var products = inventory.Products;
                if (products == null)
                {
                    Console.WriteLine("There're no products in inventory");
                }
                else
                {
                    Console.WriteLine("All products:\n" + string.Join("\n", (IEnumerable<Product>)inventory.Products) + "\n");
                }

                break;

            case MainMenuEnum.AddProductToInventory:
                productSelectionIndex = SelectItemIndexFromArray("Select item", allProducts.ToArray());
                if (productSelectionIndex != -1)
                {
                    inventory.Add(allProducts[productSelectionIndex]);
                    Console.WriteLine("Product added to inventory");
                }

                break;

            case MainMenuEnum.PrintInventoryCost:
                Console.WriteLine($"Inventory cost is {inventory.Price}");
                break;

            case MainMenuEnum.RemoveProductFromInventory:
                var invProducts = inventory.Products;
                productSelectionIndex = SelectItemIndexFromArray("Select item", invProducts);
                if (productSelectionIndex != -1)
                {
                    inventory.Remove(invProducts[productSelectionIndex]);
                    Console.WriteLine("Product removed from inventory");
                }

                break;

            case MainMenuEnum.Quit:
                Console.WriteLine("Bye...");
                return;

            default:
                throw new NotImplementedException($"Not implemented: {option}");
        }
    }
    catch (NotImplementedException ex)
    {
        throw ex;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
#pragma warning restore CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
