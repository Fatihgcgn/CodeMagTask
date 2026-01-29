using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class DomainExceptions
    {
        public sealed class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message) { }
        }

        public sealed class ConflictException : Exception
        {
            public ConflictException(string message) : base(message) { }
        }

        public sealed class ValidationException : Exception
        {
            public ValidationException(string message) : base(message) { }
        }
    }
}
