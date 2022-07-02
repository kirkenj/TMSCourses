namespace ProductInventoryProjectUsingStructures.Models
{
    internal struct ProductStruct
    {
        private static int idCounter = 0;
        private double _price = 1;
        private string _title = "Replace me";

        public ProductStruct()
        {
            this.ID = ++idCounter;
        }

        public ProductStruct(string title) : this()
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            this.Title = title;
        }

        public ProductStruct(double price) : this()
        {
            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Price can not be less or equal zero");
            }

            this.Price = price;
        }

        public ProductStruct(string title, double price) : this(price)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            this.Title = title;
        }

        public int ID
        {
            get; private set;
        }
        public string Title
        {
            get => _title;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _title = value;
                }
            }
        }
        public double Price
        {
            get => _price;
            set
            {
                if (value >= 0)
                {
                    _price = value;
                }
            }
        }

        public override string ToString()
        {
            return $"ID:{this.ID}; Title:{this._title}; Price:{this._price}";
        }
    } 
}
