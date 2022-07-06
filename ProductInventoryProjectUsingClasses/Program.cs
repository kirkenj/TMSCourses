using ProductInventoryProjectUsingClasses.Models;
using static testRepo.Programm;
using Product = ClassLibraryForHT9.Product;
using ProductInventory = ClassLibraryForHT9.ProductInventory;

List<Product> allProducts = new()
{
    new Product(),
    new Product("product2"),
    new Product(12),
    new Product("product4", 12),
};

ProductInventory inventory = new(allProducts.ToArray());
MainMenuEnum[] mainMenuEnumArr = Enum.GetValues(typeof(MainMenuEnum)).Cast<MainMenuEnum>().ToArray();
ProductCreationMenuEnum[] productCreationMenuEnumArr = Enum.GetValues(typeof(ProductCreationMenuEnum)).Cast<ProductCreationMenuEnum>().ToArray();
MainMenuEnum option;
ProductCreationMenuEnum productCreationOption;
int productSelectionIndex;

while (true)
{
    try
    {
        option = mainMenuEnumArr[SelectItemIndexFromArray("PRODUCT INVENTORY PROJECT - CLASSES", mainMenuEnumArr, false)];
        switch (option)
        {
            case MainMenuEnum.Print_all_products:
                Console.WriteLine(string.Join("\n", allProducts));
                break;
            case MainMenuEnum.Create_product:
                productCreationOption = productCreationMenuEnumArr[SelectItemIndexFromArray("PRODUCT CREATING MENU", productCreationMenuEnumArr, false)];
                switch (productCreationOption)
                {
                    #pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                    case ProductCreationMenuEnum.Create_by_title:
                        Console.WriteLine("Input product title:");
                        allProducts.Add(new Product(Console.ReadLine()));
                        break;
                    case ProductCreationMenuEnum.Create_by_price:
                        allProducts.Add(new Product(ReadIntFromConsole("Input product cost")));
                        break;
                    case ProductCreationMenuEnum.Create_by_default:
                        allProducts.Add(new Product());
                        break;
                    case ProductCreationMenuEnum.Create_by_price_and_title:
                        Console.WriteLine("Input product title:");
                        allProducts.Add(new Product(Console.ReadLine(), ReadIntFromConsole("Input product cost")));
                        break;
                    #pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                }
                
                Console.WriteLine($"Product added - {allProducts.Last()}");
                break;
            case MainMenuEnum.Print_products_in_inventory:
                Console.WriteLine("All products:\n" + string.Join("\n", (IEnumerable<Product>)inventory.Products) + "\n");
                break;
            case MainMenuEnum.Add_product_to_inventory:
                productSelectionIndex = SelectItemIndexFromArray("Select item", allProducts.ToArray());
                if (productSelectionIndex != -1)
                {
                    inventory.Add(allProducts[productSelectionIndex]);
                    Console.WriteLine("Product added to inventory");
                }

                break;
            case MainMenuEnum.Print_inventory_cost:
                Console.WriteLine($"Inventory cost is {inventory.Price}");
                break;
            case MainMenuEnum.Remove_product_from_inventory:
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