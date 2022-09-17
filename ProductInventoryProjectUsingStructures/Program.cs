using ProductInventoryProjectUsingStructures.Models;
using static testRepo.Programm;

List<ProductStruct> allProducts = new()
{
    new ProductStruct(),
    new ProductStruct("product2"),
    new ProductStruct(12),
    new ProductStruct("product4", 12),
};

const string MAIN_MENU = "PRODUCT INVENTORY PROJECT - STRUCTURES\n"+ 
    "1.Print all products\n" +
    "2.Create product\n" +
    "3.Print products in inventory\n" +
    "4.Add product to inventory\n" +
    "5.Print inventory cost\n" +
    "6.Remove product from inventory\n" +
    "7.quit";

const string PRODUCT_CREATING_MENU = "PRODUCT CREATING MENU\n" + 
    "1.Create by title\n" +
    "2.Create by price\n" +
    "3.Create by default\n" +
    "4.Create by price and title\n" +
    "5.Cancel";

ProductInventoryStruct inventory = new(allProducts.ToArray());

int option;
int productCreationOption;
int productSelectionIndex;

while (true)
{
    try
    {
        option = ReadIntFromConsole(MAIN_MENU, 1, 7);
        switch (option)
        {
            case 1:
                Console.WriteLine(string.Join("\n", allProducts));
                break;
            case 2:
                productCreationOption = ReadIntFromConsole(PRODUCT_CREATING_MENU, 0, 5);
                switch (productCreationOption)
                {
                    case 1:
                        Console.WriteLine("Input product title:");
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                        allProducts.Add(new ProductStruct(Console.ReadLine()));
                        break;
                    case 2:
                        allProducts.Add(new ProductStruct(ReadIntFromConsole("Input product cost")));
                        break;
                    case 3:
                        allProducts.Add(new ProductStruct());
                        break;
                    case 4:
                        Console.WriteLine("Input product title:");
                        allProducts.Add(new ProductStruct(Console.ReadLine(), ReadIntFromConsole("Input product cost")));
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                        break;
                }
                
                Console.WriteLine($"Product added - {allProducts.Last()}");
                break;
            case 3:
                Console.WriteLine(string.Join("\n", (IEnumerable<ProductStruct>)inventory.Products));
                break;
            case 4:
                
                productSelectionIndex = SelectItemIndexFromArray("Select item", allProducts.ToArray());
                if (productSelectionIndex != -1)
                {
                    inventory.Add(allProducts[productSelectionIndex]);
                    Console.WriteLine("Product added to inventory");
                }

                break;
            case 5:
                Console.WriteLine($"Inventory cost is {inventory.Price}");
                break;
            case 6:
                var invProducts = inventory.Products;
                productSelectionIndex = SelectItemIndexFromArray("Select item", invProducts);
                if (productSelectionIndex != -1)
                {
                    inventory.Remove(invProducts[productSelectionIndex]);
                    Console.WriteLine("Product removed from inventory");
                }

                break;
            default:
                Console.WriteLine("Bye...");
                return;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}