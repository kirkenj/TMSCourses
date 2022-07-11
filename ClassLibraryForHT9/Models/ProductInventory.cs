namespace ClassLibraryForHT9.Models
{
    public class ProductInventory
    {
        private readonly List<Product> _products;

        public ProductInventory()
        {
            _products = new List<Product>();
        }

        public ProductInventory(Product product) : this()
        {
            Add(product ?? throw new ArgumentNullException(nameof(product)));
        }

        public ProductInventory(params Product[] products) : this()
        {
            Add(products ?? throw new ArgumentNullException(nameof(products)));
        }

        public double Price => _products.Sum(x => x.Price);
        public Product[] Products => _products.ToArray();
        public void Add(Product product) => _products.Add(product ?? throw new ArgumentNullException(nameof(product)));
        public void Add(params Product[] range) => this._products.AddRange(range ?? throw new ArgumentNullException(nameof(range)));
        public void Remove(Product product) => _products.Remove(product ?? throw new ArgumentNullException(nameof(product)));
    }
}
