namespace HT13.Models
{
    public abstract class Genus : Family
    {
        public string GenusName { get; set; }

        public Genus(string domainName, string kingdomName, string phylumName, string className, string orderName, string familyName, string name)
            : base(domainName, kingdomName, phylumName, className, orderName, familyName)
        {
            GenusName = name;
        }

        public override string ToString()
        {
            return base.ToString() + $"Genus - {GenusName}; ";
        }
    }
}
