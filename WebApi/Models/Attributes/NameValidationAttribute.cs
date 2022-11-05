using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Attributes
{
    public class NameValidationAttribute : ValidationAttribute
    {
        private readonly int _maxWordsAmm;
        private readonly char _separator;

        public NameValidationAttribute(int maxWordsAmm, char separator)
        {
            _maxWordsAmm = maxWordsAmm;
            _separator = separator;
            ErrorMessage = $"Contains of {maxWordsAmm + 1} or more words with separator '{separator}'";
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            return value.ToString().Split(new[] {_separator}).Length <= _maxWordsAmm;
        }
    }
}
