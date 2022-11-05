using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Attributes
{
    public class BannedWordsAttribute : ValidationAttribute
    {
        private readonly string[] _bannedSymbols;
        
        public BannedWordsAttribute(string[] bannedSymbols)
        {
            _bannedSymbols = bannedSymbols;
            ErrorMessage = $"Contains 1 or more substrings from [{string.Join(", ", _bannedSymbols)}]";
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            var lowerValue = value.ToString()?.ToLower() ?? "";
            return !_bannedSymbols.Any(b=>lowerValue.Contains(b.ToLower()));
        }
    }
}
