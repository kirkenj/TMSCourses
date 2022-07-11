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
                ProductCreationMenuEnum.CreateByTitle => "Create_by_title".Replace('_', ' '),
                ProductCreationMenuEnum.CreateByPrice => "Create_by_price".Replace('_', ' '),
                ProductCreationMenuEnum.CreateByDefault => "Create_by_default".Replace('_', ' '),
                ProductCreationMenuEnum.CreateByPriceAndTitle => "Create_by_price_and_title".Replace('_', ' '),
                ProductCreationMenuEnum.Cancel => "Cancel",
                _ => throw new ArgumentException("Value not supported", nameof(value))
            };
        }
    }
}
