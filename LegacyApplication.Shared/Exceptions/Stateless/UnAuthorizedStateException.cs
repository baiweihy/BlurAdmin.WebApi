using System;

namespace LegacyApplication.Shared.Exceptions.Stateless
{
    public class UnAuthorizedStateException : Exception
    {
        public UnAuthorizedStateException() { }
        public UnAuthorizedStateException(string message) : base(message) { }
        public UnAuthorizedStateException(string message, Exception inner) : base(message, inner) { }
    }
}
