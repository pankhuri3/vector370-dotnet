using System;
using System.Collections.Generic;

namespace ThreeSeventy.Vector.Client.Tests
{

    class MockRetryConfig : IRetryConfig
    {
        public Type Type
        {
            get { return typeof(IncrementalRetryStrategy); }
            set { }
        }

        public int MaxTries
        {
            get { return 3; }
            set { }
        }

        public TimeSpan MinInterval
        {
            get { return TimeSpan.FromMilliseconds(500); }
            set { }
        }

        public TimeSpan MaxInterval
        {
            get { return TimeSpan.FromSeconds(10); }
            set { }
        }

        public TimeSpan Interval
        {
            get { return TimeSpan.FromMilliseconds(500); }
            set { }
        }
    }
}
