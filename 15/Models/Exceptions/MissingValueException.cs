namespace _15.Models.Exceptions
{
    public class MissingValueException : ArgumentNullException
    {
        public MissingValueException(string? paramName, string? message) : base(paramName, message) { }
        public MissingValueException(string? paramName) : base(paramName) { }
    }
}
