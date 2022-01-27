using System;

namespace Business.Exceptions
{
    /// <summary>
    /// BadRequestException to return 400 status code
    /// </summary>
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
        : base(message) { }
    }
}