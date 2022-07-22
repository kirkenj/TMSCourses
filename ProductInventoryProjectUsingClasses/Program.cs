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

Product forNewProductBuffer;
Product forRemoveProductBuffer;
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
                        Console.WriteLine("Input product title\n(if you leave the string empty, the value will be null):");
                        inventory.Add(new Product(Console.ReadLine()));
                        break;
                    case ProductCreationMenuEnum.CreateByPrice:
                        inventory.Add(new Product(ReadIntFromConsole("Input product cost")));
                        break;
                    case ProductCreationMenuEnum.CreateByDefault:
                        inventory.Add(new Product());
                        break;
                    case ProductCreationMenuEnum.CreateByPriceAndTitle:
                        Console.WriteLine("Input product title\n(if you leave the string empty, the value will be null)::");
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

            case MainMenuEnum.FindProductByID:
                Console.WriteLine(inventory[ReadIntFromConsole("Input products ID")]?.ToString() ?? "Not found");

                break;

            case MainMenuEnum.ReplaceFoundProductByIDWithNew:
                var theID = ReadIntFromConsole("Input products ID");
                forRemoveProductBuffer = inventory[theID];
                if (forRemoveProductBuffer == null)
                {
                    Console.WriteLine("Not found");
                    continue;
                }

                forNewProductBuffer = new Product();
                Console.WriteLine($"Product {inventory[theID]} was replaced with {forNewProductBuffer}");
                inventory[theID] = forNewProductBuffer;
                break;

            case MainMenuEnum.ReplaceFoundProductByTitleWithNew:
                Console.WriteLine("Input product's title");
                var theTitle = Console.ReadLine();
                forRemoveProductBuffer = inventory[theTitle];
                if (forRemoveProductBuffer == null)
                {
                    Console.WriteLine("Not found");
                    continue;
                }

                forNewProductBuffer = new Product();
                Console.WriteLine($"Product {inventory[theTitle]} was replaced with {forNewProductBuffer}");
                inventory[theTitle] = forNewProductBuffer;
                break;

            case MainMenuEnum.FindProductByTitle:
                Console.WriteLine("Input product's title");
                var titile = Console.ReadLine();
                Console.WriteLine(inventory[titile]?.ToString() ?? "Not found");

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
