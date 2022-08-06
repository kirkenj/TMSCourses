namespace HT13.Models
{
    public abstract class Domain
    {
        public string DomainName { get; set; }
        public Domain(string name)
        {
            DomainName = name;
        }

        public override string ToString() => $"Domain - {DomainName}; ";
    }
}
