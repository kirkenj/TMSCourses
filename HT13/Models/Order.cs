namespace HT13.Models
{
    public abstract class Order : Class
    {
        public string OrderName { get; set; }

        public Order(string domainName, string kingdomName, string phylumName, string className, string name)
            : base(domainName, kingdomName, phylumName, className)
        {
            OrderName = name;
        }

        public override string ToString()
        {
            return base.ToString() + $"Order - {OrderName}; ";
        }
    }
}
