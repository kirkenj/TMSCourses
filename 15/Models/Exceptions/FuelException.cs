using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15.Models.Exceptions
{
    public class FuelException : ArgumentException
    {
        public FuelException(string? message) : base(message) { }
        public FuelException(string? message, string? paramName) : base(message, paramName) { }
    }
}
