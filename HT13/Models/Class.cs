namespace HT13.Models
{
    public abstract class Class : Phylum
    {
        public string ClassName { get; set; }

        public Class(string domainName, string kingdomName, string phylumName, string name)
            : base(domainName, kingdomName, phylumName)
        {
            ClassName = name;
        }

        public override string ToString()
        {
            return base.ToString() + $"Class - {ClassName}; ";
        }
    }
}
