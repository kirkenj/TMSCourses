namespace _15.Models.Exceptions
{
    public class InvalidValueException : ArgumentOutOfRangeException
    {
        public InvalidValueException(string? paramName) : base(paramName) { }
        public InvalidValueException(string? paramName, string? message) : base(paramName, message) { }
    }
}
