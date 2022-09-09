namespace HT13.Models
{
    public abstract class Phylum : Kingdom
    {
        public string PhylumName { get; set; }

        public Phylum(string domainName, string kingdomName, string name) 
            : base(domainName, kingdomName)
        {
            PhylumName = name;
        }

        public override string ToString()
        {
            return base.ToString() + $"Phylum - {PhylumName}; ";
        }
    }
}
