namespace HT13.Models
{
    public abstract class Family : Order
    {
        public string FamilyName { get; set; }

        public Family(string domainName, string kingdomName, string phylumName, string className, string orderName, string name)
            : base(domainName, kingdomName, phylumName, className, orderName)
        {
            FamilyName = name;
        }

        public override string ToString()
        {
            return base.ToString() + $"Family - {FamilyName}; ";
        }
    }
}
