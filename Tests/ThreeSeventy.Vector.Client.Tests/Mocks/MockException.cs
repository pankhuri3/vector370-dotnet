using System;
using System.Collections.Generic;

namespace ThreeSeventy.Vector.Client.Tests
{
    class MockException : Exception
    {
        public MockException(string message)
            : base(message)
        {

        }
    }
}
