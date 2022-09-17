namespace HT13.Models
{
    public abstract class Kingdom : Domain
    {
        public string KingdomName { get; set; }

        public Kingdom(string domainName, string name) : base(domainName)
        {
            KingdomName = name;
        }

        public override string ToString()
        {
            return base.ToString() + $"Kinhdom - {KingdomName}; ";
        }
    }
}
