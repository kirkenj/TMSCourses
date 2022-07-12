using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForHT9.Models.Enums
{
    public enum ProductCreationMenuEnum {
        CreateByTitle = 1,
        CreateByPrice = 2,
        CreateByDefault = 3,
        CreateByPriceAndTitle = 4,
        Cancel 
    }

    public static class ProductCreationMenuEnumConvertor
    {
        public static string ToString(ProductCreationMenuEnum value)
        {
            return value switch
            {
                ProductCreationMenuEnum.CreateByTitle => "Create by title",
                ProductCreationMenuEnum.CreateByPrice => "Create by price",
                ProductCreationMenuEnum.CreateByDefault => "Create by default",
                ProductCreationMenuEnum.CreateByPriceAndTitle => "Create by price and title",
                ProductCreationMenuEnum.Cancel => "Cancel",
                _ => throw new ArgumentException("Value not supported", nameof(value))
            };
        }
    }
}
