using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Attributes
{
    public class BannedSymbolsAttribute : ValidationAttribute
    {
        private readonly char[] _bannedSymbols;
        
        public BannedSymbolsAttribute(char[] bannedSymbols)
        {
            _bannedSymbols = bannedSymbols;
            ErrorMessage = $"Contains 1 or more symbols from [{string.Join(", ", _bannedSymbols)}]";
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            return !_bannedSymbols.Any(b=>value.ToString().Contains(b));
        }
    }
}
