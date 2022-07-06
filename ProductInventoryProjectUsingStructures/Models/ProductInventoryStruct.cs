namespace ProductInventoryProjectUsingStructures.Models
{
    internal struct ProductInventoryStruct
    {
        private readonly List<ProductStruct> _products;

        public ProductInventoryStruct()
        {
            _products = new List<ProductStruct>();
        }

        public ProductInventoryStruct(ProductStruct product) : this()
        {
            Add(product);
        }

        public ProductInventoryStruct(params ProductStruct[] products) : this()
        {
            Add(products);
        }

        public void Add(ProductStruct product)
        {
            _products.Add(product);
        }

        public void Add(params ProductStruct[] range)
        {
            foreach (var product in range)
            {
                this.Add(product);
            }
        }

        public double Price
        {
            get => _products.Sum(x => x.Price);
        }

        public ProductStruct[] Products { get => _products.ToArray(); }

        public void Remove(ProductStruct product)
        {
            _products.Remove(product);
        }
    }
}
