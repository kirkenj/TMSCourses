namespace ProductInventoryProjectUsingStructures.Models
{
    internal struct ProductInventory
    {
        private List<Product> _products;

        public ProductInventory()
        {
            _products = new List<Product>();
        }

        public ProductInventory(Product product) : this()
        {
            Add(product);
        }

        public ProductInventory(params Product[] products) : this()
        {
            Add(products);
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Add(params Product[] range)
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

        public Product[] Products { get => _products.ToArray(); }

        public void Remove(Product product)
        {
            _products.Remove(product);
        }
    }
}
