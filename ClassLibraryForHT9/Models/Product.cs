namespace ClassLibraryForHT9.Models
{
    public class Product
    {
        private static int _idCounter = 0;
        private double _price = 1;
        private string _title = "Replace me";

        public Product()
        {
            this.ID = ++_idCounter;
        }

        public Product(string title) : this()
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            this._title = title;
        }

        public Product(double price) : this()
        {
            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Price can not be less or equal zero");
            }

            this._price = price;
        }

        public Product(string title, double price) : this(price)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            this._title = title;
        }

        public int ID
        {
            get;
        }

        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _title = value.Trim();
            }
        }
        public double Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _price = value;
            }
        }

        public override string ToString() => $"ID:{this.ID}; Title:{this._title}; Price:{this._price}";
    } 
}
