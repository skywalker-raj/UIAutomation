using System;
namespace Zakipoint.Framework.Driver
{
    public class BrowserException : Exception
    {
        public BrowserException()
        {
        }
        public BrowserException(string message)
            : base(message)
        {
        }
    }
}