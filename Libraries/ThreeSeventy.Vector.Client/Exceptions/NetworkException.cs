using System;
using System.Collections.Generic;

namespace ThreeSeventy.Vector.Client
{
    /// <summary>
    /// Represents an network communication error when attempting to reach the REST server.
    /// </summary>
    /// <remarks>
    /// This is mostly used as a differentiator between a generic exception and an error that
    /// has occurred when attempting to communicate with the server.
    /// 
    /// The retry logic uses this to retry on network errors.
    /// </remarks>
    public class NetworkException : Exception
    {
        /// <summary />
        public NetworkException(string errorMessage, Exception errorException)
            : base(errorMessage, errorException)
        {
        }
    }
}
