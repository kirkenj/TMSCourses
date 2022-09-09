namespace HT13.Models
{
    public class Species : Genus
    {
        public string SpeciesName { get; set; }

        public Species(string domainName, string kingdomName, string phylumName, string className, string orderName, string familyName, string genusName, string name)
            : base(domainName, kingdomName, phylumName, className, orderName, familyName, genusName)
        {
            SpeciesName = name;
        }

        public override string ToString()
        {
            return base.ToString() + $"Species - {SpeciesName}; ";
        }
    }
}
