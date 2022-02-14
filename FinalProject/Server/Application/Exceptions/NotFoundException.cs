using System;

namespace Application.Exceptions
{
    /// <summary>
    /// NotFoundException to return 404 status code
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
        : base(message) { }
    }
}
