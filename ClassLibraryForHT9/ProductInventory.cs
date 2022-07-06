namespace ClassLibraryForHT9
{
    public class ProductInventory
    {
        private List<Product> _products;

        public ProductInventory()
        {
            _products = new List<Product>();
        }

        public ProductInventory(Product product) : this()
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            Add(product);
        }

        public ProductInventory(params Product[] products) : this()
        {
            Add(products);
        }

        public void Add(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

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
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _products.Remove(product);
        }
    }
}
