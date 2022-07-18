namespace ClassLibraryForHT9.Models
{
    public class Product
    {
        /// пример документационного комментария
        /// <summary>
        /// статический счетчик для идентификаторов
        /// </summary>
        private static int _idCounter = 0;
        public static readonly string DefaultTitleValue = "Default or null";
        private double _price = 1;
        private string? _title = null;
        public Product()
        {
        }

        public Product(string? title)
        {
            _title = string.IsNullOrWhiteSpace(title) ? null : title.Trim();
        }

        public Product(double price)
        {
            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Price can not be less or equal zero");
            }

            _price = price;
        }

        public Product(string? title, double price): this(price)
        {
            _title = string.IsNullOrWhiteSpace(title) ? null : title.Trim();
        }

        public int ID
        {
            get;
        } = ++_idCounter;

        /// <summary>
        /// поле названия. метод set допускает значение null. В случае, если полю присвоено значение null, get вернет значение Product.DefaultTitleValue
        /// </summary> 
        public string Title
        {
            get => _title ?? DefaultTitleValue;
            set
            {
                _title = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
            }
        }

        internal double Price
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

        public override string ToString() => $"ID:{this.ID}; Title:{this.Title}; Price:{this._price}";
    } 
}
