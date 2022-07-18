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
            if (product != null)
            {
                Add(product);
            }
        }

        public ProductInventory(params Product[] products) : this()
        {
            if (products != null)
            {
                Add(products);
            }
        }
        public Product? this[int index]
        {
            get => _products.FirstOrDefault(p=>p.ID == index);
            set
            {
                var productToRemove = _products[index];
                if (productToRemove != value)
                {
                    Remove(productToRemove);
                    if (value != null)
                    {
                        Add(value);
                    }
                }
            }
        }

        public Product? this[string? productTitle]
        {
            get => _products.FirstOrDefault(p=>p.Title == (string.IsNullOrWhiteSpace(productTitle) ? Product.DefaultTitleValue : productTitle.Trim()));
            set
            {
                var productToRemove = this[productTitle];
                if (productToRemove != null && productToRemove != value)
                {
                    Remove(productToRemove);
                    if (value != null)
                    {
                        Add(value);
                    }
                }
            }
        }

        public double Price => _products.Sum(x => x.Price);
        public Product[]? Products => _products.Any() ? _products.ToArray() : null;
        public void Remove(Product product) => _products.Remove(product);
        public void Add(Product product) => _products.Add(product ?? throw new ArgumentNullException(nameof(product)));
        public void Add(params Product[] range)
        {
            if (range == null)
            {
                return;
            }

            foreach (var item in range)
            {
                if (item != null)
                {
                    _products.Add(item);
                }
            }
        }
    }
}
